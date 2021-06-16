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
            else
            {
                throw new NotImplementedException();
            }
        }

        public static List<DataGridViewColumn> CreateColumnHeadersFromType(Type headerType, Action<DataGridViewColumn> customizedColumnFuction = null)
        {
            List<DataGridViewColumn> result = new List<DataGridViewColumn>();

            var properties = headerType.GetProperties();
            foreach (var property in properties)
            {
                if (property.Name.Equals("RuntimePropertyInfo", StringComparison.OrdinalIgnoreCase))
                    continue;
                
                var column = GetColumnType(property.PropertyType);
                column.Name = property.Name;

                var uiMap = property.GetCustomAttribute(typeof(UIMapAttribute)) as UIMapAttribute;

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
    }
}
