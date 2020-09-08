using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using Winforsys.DBManagers;

namespace WinforsysGlassMap
{
    public class RawData
    {
        public class P_RAW
        {
            public string PNL_ID { get; set; }
            public string PNL_ORIGIN { get; set; }
            public double PNL_POINT_X { get; set; }
            public double PNL_POINT_Y { get; set; }
            public double PNL_SIZE_X { get; set; }
            public double PNL_SIZE_Y { get; set; }

            public double PNL_MARGIN_X { get; set; }
            public double PNL_MARGIN_Y { get; set; }
            public double PNL_F_SIZE_X { get; set; }
            public double PNL_F_SIZE_Y { get; set; }

            public double PNL_DATA { get; set; }
            public double PNL_GATE { get; set; }

            public object TAG = null;
        }
                
        public string GLS_ID { get; set; }
        public string Product_Id { get; set; }

        public List<P_RAW> PNL_RAW_DATA = new List<P_RAW>();

        public List<P_RAW> PNL_NAME_DATA = new List<P_RAW>();
        public void Load(string productId)
        {
            StringBuilder query = new StringBuilder();

            query.AppendFormat("SELECT \n");
            query.AppendFormat("PNL_ID, PNL_ORIGIN, PNL_POINT_X, PNL_POINT_Y, PNL_SIZE_X, PNL_SIZE_Y \n");
            query.AppendFormat("FROM WIN_RCP.TMS_GLS_MAP_TBL \n");
            query.AppendFormat("WHERE Product_Id = '{0}' \n", productId);
            query.AppendFormat("ORDER BY PNL_ID ASC \n");

            DataSet ds = new DataSet();
            OracleDBManager.Instance.ExecuteDsQuery(ds, query.ToString());

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                productId = "DEFAULT";

            query.Length = 0;
            query.AppendFormat("SELECT \n");
            query.AppendFormat("PNL_ID, PNL_ORIGIN, PNL_POINT_X, PNL_POINT_Y, PNL_SIZE_X, PNL_SIZE_Y \n");
            query.AppendFormat("FROM WIN_RCP.TMS_GLS_MAP_TBL \n");
            query.AppendFormat("WHERE Product_Id = '{0}' \n", productId);
            query.AppendFormat("ORDER BY PNL_ID ASC \n");

            ds = new DataSet();
            OracleDBManager.Instance.ExecuteDsQuery(ds, query.ToString());

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return;

            PNL_RAW_DATA = new List<P_RAW>();

            for (int row = 0; row < ds.Tables[0].Rows.Count; ++row)
            {
                int col = 0;

                P_RAW p = new P_RAW();

                p.PNL_ID = ds.Tables[0].Rows[row][col++].ToString();
                p.PNL_ORIGIN = ds.Tables[0].Rows[row][col++].ToString();

                double pX = 0;
                double pY = 0;
                double sX = 0;
                double sY = 0;

                double.TryParse(ds.Tables[0].Rows[row][col++].ToString(), out pX);
                double.TryParse(ds.Tables[0].Rows[row][col++].ToString(), out pY);
                double.TryParse(ds.Tables[0].Rows[row][col++].ToString(), out sX);
                double.TryParse(ds.Tables[0].Rows[row][col++].ToString(), out sY);

                p.PNL_POINT_X = pX;
                p.PNL_POINT_Y = pY;
                p.PNL_SIZE_X = sX;
                p.PNL_SIZE_Y = sY;

                PNL_RAW_DATA.Add(p);
            }
        }
        public void Load(string productId, string dept)
        {
            string query = string.Empty;

            query = @"
SELECT PNL_ID, PNL_ORIGIN, PNL_POINT_X, PNL_POINT_Y, PNL_SIZE_X, PNL_SIZE_Y
FROM WIN_RCP_#DEPT.#DEPT_GLS_MAP_TBL
WHERE Product_Id = '#PRODUCTID'
ORDER BY PNL_ID ASC
";
            query = query.Replace("#DEPT", dept);
            query = query.Replace("#PRODUCTID", productId);

            DataSet ds = new DataSet();
            OracleDBManager.Instance.ExecuteDsQuery(ds, query);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return;

            PNL_RAW_DATA = new List<P_RAW>();

            for (int row = 0; row < ds.Tables[0].Rows.Count; ++row)
            {
                int col = 0;

                P_RAW p = new P_RAW();

                p.PNL_ID = ds.Tables[0].Rows[row][col++].ToString();
                p.PNL_ORIGIN = ds.Tables[0].Rows[row][col++].ToString();

                double pX = 0;
                double pY = 0;
                double sX = 0;
                double sY = 0;

                double.TryParse(ds.Tables[0].Rows[row][col++].ToString(), out pX);
                double.TryParse(ds.Tables[0].Rows[row][col++].ToString(), out pY);
                double.TryParse(ds.Tables[0].Rows[row][col++].ToString(), out sX);
                double.TryParse(ds.Tables[0].Rows[row][col++].ToString(), out sY);

                p.PNL_POINT_X = pX;
                p.PNL_POINT_Y = pY;
                p.PNL_SIZE_X = sX;
                p.PNL_SIZE_Y = sY;

                PNL_RAW_DATA.Add(p);
            }
        }

        public void LoadFormGlsSeq(string glsSeq, string dept = "BP")
        {
            string query = @"
SELECT
SUBSTR(PNL_ID, 13, 5), PNL_Gd_Start,
PNL_X_POS, PNL_Y_POS, PNL_X_SIZE, PNL_Y_SIZE
FROM WIN_JPC_#DEPT.#DEPT_AOI_PNL_HIS_TBL
WHERE GLS_Seq = #GLSSEQ
ORDER BY Pnl_Id ASC
";
            query = query.Replace("#DEPT", dept);
            query = query.Replace("#GLSSEQ", glsSeq);            

            DataSet ds = new DataSet();
            OracleDBManager.Instance.ExecuteDsQuery(ds, query.ToString());

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return;

            PNL_RAW_DATA = new List<P_RAW>();

            for (int row = 0; row < ds.Tables[0].Rows.Count; ++row)
            {
                int col = 0;

                P_RAW p = new P_RAW();

                p.PNL_ID = ds.Tables[0].Rows[row][col++].ToString();
                p.PNL_ORIGIN = ds.Tables[0].Rows[row][col++].ToString();

                double pX = 0;
                double pY = 0;
                double sX = 0;
                double sY = 0;

                double.TryParse(ds.Tables[0].Rows[row][col++].ToString(), out pX);
                double.TryParse(ds.Tables[0].Rows[row][col++].ToString(), out pY);
                double.TryParse(ds.Tables[0].Rows[row][col++].ToString(), out sX);
                double.TryParse(ds.Tables[0].Rows[row][col++].ToString(), out sY);

                p.PNL_POINT_X = pX * 1000;
                p.PNL_POINT_Y = pY * 1000;
                p.PNL_SIZE_X = sX * 1000;
                p.PNL_SIZE_Y = sY * 1000;

                PNL_RAW_DATA.Add(p);
            }
        }
    }
}
