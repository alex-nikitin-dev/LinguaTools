using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
namespace LinguaHelper
{
    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        private int _columnToSort;

        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder _orderOfSort;

        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private readonly CaseInsensitiveComparer _objectCompare;


        public List<Type> ColumnTypes { get; set; }
        public string DateTimeFormat { get; set; }

        /// <summary>
        /// Class constructor. Initializes various elements
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            _columnToSort = 0;

            // Initialize the sort order to 'none'
            _orderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            _objectCompare = new CaseInsensitiveComparer();

            ColumnTypes = new List<Type>();
        }

        private DateTime ParseDateTime(string data)
        {
            return DateTime.ParseExact(data, DateTimeFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// This method is inherited from the IComparer interface. It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            // Cast the objects to be compared to ListViewItem objects
            var listViewX = (ListViewItem)x;
            var listViewY = (ListViewItem)y;

            // Compare the two items
            Debug.Assert(listViewY != null, nameof(listViewY) + " != null");

            int compareResult = 0;

            if (ColumnTypes[_columnToSort] == typeof(string))
            {
                compareResult = _objectCompare.Compare(listViewX?.SubItems[_columnToSort].Text, listViewY.SubItems[_columnToSort].Text);
            }
            else if (ColumnTypes[_columnToSort] == typeof(DateTime))
            {
                compareResult = DateTime.Compare(ParseDateTime(listViewX?.SubItems[_columnToSort].Text), ParseDateTime(listViewY.SubItems[_columnToSort].Text));
            }

            // Calculate correct return value based on object comparison
            if (_orderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }

            if (_orderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }

            // Return '0' to indicate they are equal
            return 0;
        }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set => _columnToSort = value;
            get => _columnToSort;
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set => _orderOfSort = value;
            get => _orderOfSort;
        }

    }
}
