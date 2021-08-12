using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page {
    protected void Page_Init(object sender, EventArgs e) {
        GridView1.DataSource = new List<MyDataRow> {
            new MyDataRow{ Id = 1, Title = "TitleA" },
            new MyDataRow{ Id = 2, Title = "" , Notes = "This title has empty string value"},
            new MyDataRow{ Id = 3, Title = null, Notes = "This title has NULL value" }
        };

        GridView1.DataBind();
    }

    public class MyDataRow {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
    }



    protected void GridView1_SubstituteFilter(object sender, DevExpress.Data.SubstituteFilterEventArgs e) {
        var cols = CriteriaColumnAffinityResolver.SplitByColumns(e.Filter);
        var isEqualsOperator = cols.Select(a => a.Value).OfType<BinaryOperator>().Where(uo => uo.OperatorType == BinaryOperatorType.Equal && (uo.RightOperand as OperandValue).Value == null).ToList();
        if (isEqualsOperator.Count != 0) {
            isEqualsOperator.ForEach(i => {
                OperandProperty op = (i.LeftOperand as OperandProperty);
                string fn = op.PropertyName;
                BinaryOperator nbo = new BinaryOperator(fn, string.Empty, BinaryOperatorType.Equal);
                cols[op] = GroupOperator.Or(new CriteriaOperator[] { nbo, cols[op] });
            });
        }
        e.Filter = GroupOperator.And(cols.Values);
    }
}