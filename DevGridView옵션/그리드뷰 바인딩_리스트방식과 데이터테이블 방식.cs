public List<ProductInfo> DBSelect()
{
    List<ProductInfo> productList = new List<ProductInfo>();
    string sql = "SELECT * FROM PInformation";

    using (cmd = new SqlCommand(sql, conn))
    using (SqlDataReader read = cmd.ExecuteReader())
    {
        while (read.Read())
        {
            productList.Add(new ProductInfo
            {
                productCode = read["productCode"].ToString(),
                productionDate = read["productionDate"].ToString(),
                expiryDate = read["expiryDate"].ToString(),
                productionNumber = read["productionNumber"].ToString(),
                lotNum = read["lotNum"].ToString(),
                barCode = read["barCode"].ToString(),
                productionTime = read["productionTime"].ToString(),
                labelStatus = read["labelStatus"].ToString()
            });
        }
    }
    return productList;
}

// 바인딩 예시 (DevExpress GridControl)
gridControl1.DataSource = db.DBSelect();

====================================================
====================================================
public DataTable DBSelect()
{
    DataTable dt = new DataTable();
    string sql = "SELECT * FROM PInformation";

    using (cmd = new SqlCommand(sql, conn))
    using (SqlDataReader read = cmd.ExecuteReader())
    {
        dt.Load(read);  // SqlDataReader의 데이터를 DataTable로 한 번에 로드
    }
    return dt;
}

// 바인딩 예시 (DataGridView)
gridControl1.DataSource = db.DBSelect();
