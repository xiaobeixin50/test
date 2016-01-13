using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoldenPigs.Controls
{


    public class myDataGridView : DataGridView
    {
        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = !this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected;
            
        }

        protected override void OnCellMouseClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseClick(e);
        }

    }
}
