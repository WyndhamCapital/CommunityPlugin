using CommunityPlugin.Objects.Attributes;
using EllieMae.EMLite.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunityPlugin.Objects.Models.WCM.FieldExtraction;
using EllieMae.EMLite.ClientServer;

namespace CommunityPlugin.Objects.Helpers
{
    public static class GridViewHelper
    {
        public static DataGridViewColumn GetColumnType(Type propertyType)
        {
            // return type of column based on property type
            if (propertyType == typeof(string) ||
                propertyType == typeof(int))
            {
                return new DataGridViewTextBoxColumn();
            }

            else if (propertyType == typeof(bool))
            {
                return new DataGridViewCheckBoxColumn();
            }
            // SP - custom object is property in classified doc for data extraction
            // for UI, we are going to present the number of fields
            else if (propertyType == typeof(IEnumerable<IFieldData>))
            {
                return new DataGridViewTextBoxColumn();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static object[] MapObjectToDataGridRowForUi(DataGridView gridView, object objectToMap)
        {
            object[] result = new object[gridView.Columns.Count];


            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                var column = gridView.Columns[i];
                result[i] = ReflectHelper.GetPropValue(objectToMap, column.Name);
            }

            return result;
        }

        public static List<DataGridViewColumn> CreateColumnHeadersFromType(Type headerType, Action<DataGridViewColumn> customizedColumnFuction = null)
        {
            List<DataGridViewColumn> result = new List<DataGridViewColumn>();

            var interfaceTypes = headerType.GetInterfaces().ToList();
            List<Type> allTypes = new List<Type>();
            allTypes.AddRange(interfaceTypes);
            allTypes.Add(headerType);

            foreach (var type in allTypes)
            {
                var properties = type.GetProperties();
                foreach (var property in properties)
                {
                    if (property.Name.Equals("RuntimePropertyInfo", StringComparison.OrdinalIgnoreCase))
                        continue;

                    var uiMap = property.GetCustomAttribute(typeof(UIMapAttribute)) as UIMapAttribute;
                    if (uiMap != null && uiMap.Ignore)
                    {
                        continue; // go to next proprety
                    }

                    var column = GetColumnType(property.PropertyType);
                    column.Name = property.Name;

                    if (uiMap == null)
                    {
                        // use property name
                        column.HeaderText = property.Name;
                    }
                    else
                    {
                        // use ui map name
                        // uiMap.DisplayName
                        if (string.IsNullOrWhiteSpace(uiMap.DisplayName))
                        {
                            column.HeaderText = property.Name;
                        }
                        else
                        {
                            column.HeaderText = uiMap.DisplayName;
                        }

                        column.Visible = uiMap.Editable;

                    }

                    if (customizedColumnFuction != null)
                    {
                        try
                        {
                            customizedColumnFuction(column);
                        }
                        catch (Exception ex)
                        {
                            throw new NotImplementedException();
                            // log info
                        }
                    }

                    result.Add(column);
                }
            }
     

            return result;
        }

        public static T SetRowObjectPropertiesFromGridViewColumns<T>(T result, DataGridViewRow row, DataGridView dataGridView)
        {
            foreach (PropertyInfo propertyInfo in result.GetType().GetProperties())
            {
                // SP - check if a column with this name exists first
                var column = GridViewHelper.GetColumnByName(dataGridView, propertyInfo.Name);
                if (column != null)
                {
                    var value = row.Cells[propertyInfo.Name].Value;
                    if (propertyInfo != null && propertyInfo.CanWrite)
                    {
                        if (value == null)
                            propertyInfo.SetValue(result, null, null);

                        else if (value != null && value != System.DBNull.Value)
                            propertyInfo.SetValue(result, value, null);
                    }
                }
            }

            return result;
        }
        internal static DataGridViewColumn GetColumnByName(DataGridView gridView, string columnName)
        {
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                var column = gridView.Columns[i];
                if (column.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return column;
                }
            }

            return null;
        }

        internal static string[] GetPastedRowsFromClipboard()
        {
            DataObject o = (DataObject)Clipboard.GetDataObject();
            if (o.GetDataPresent(DataFormats.StringFormat))
            {
                string[] pastedRows = Regex.Split(o.GetData(DataFormats.StringFormat).ToString().TrimEnd("\r\n".ToCharArray()), "\r");
                return pastedRows;
            }

            return null;
        }

        public static IDictionary<GVItem, T> GetAllEncompassGridViewItems<T>(GridView gv)
        {
            IDictionary<GVItem, T> result = new Dictionary<GVItem, T>();
            foreach (GVItem row in gv.Items)
            {
                var document = (T)row.Tag;
                result.Add(row, document);
            }

            return result;
        }

        public static void LoadListObjectsToGridview<T>(DataGridView gridview, IList<T> listObjects)
        {
            gridview.Rows.Clear();

            foreach (var obj in listObjects)
            {
                int fieldIndex = gridview.Rows.Add(GridViewHelper.MapObjectToDataGridRowForUi(gridview, obj));
                gridview.Rows[fieldIndex].Tag = obj;
            }
        }

        public static T GetCurrentGridRowTag<T>(DataGridView dgv, out string errorMsg)
        {
            errorMsg = string.Empty;
            var currentRow = GetCurrentGridRow(dgv, out errorMsg);
            if (currentRow == null)
            {
                return default(T);
            }

            return (T)currentRow.Tag;
        }

        public static DataGridViewRow GetCurrentGridRow(DataGridView dgv, out string errorMsg)
        {
            errorMsg = null;

            var selectedCell = dgv.CurrentCell;
            if (selectedCell == null)
            {
                errorMsg = "No Row is selected, please try again.";
                return null;
            }

            DataGridViewRow row = dgv.Rows[dgv.CurrentCell.RowIndex];
            if (row.IsNewRow)
            {
                errorMsg = "No Row is selected, please try again.";
                return null;
            }

            return row;
        }

        public static void LoadFieldMappingColumns<T>(DataGridView gridView)
        {
            var columnHeaders = GridViewHelper.CreateColumnHeadersFromType(typeof(T), (column) =>
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells);

            gridView.Columns.AddRange(columnHeaders.ToArray());
        }
    }
}
