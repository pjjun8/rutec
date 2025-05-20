using (SqlConnection conn = new SqlConnection(connStr))
{
    conn.Open();

    // 1. 임시 테이블 생성
    string createTemp = @"CREATE TABLE #Temp (RUSIACODE_CODE NVARCHAR(100), COL2 NVARCHAR(50))";
    new SqlCommand(createTemp, conn).ExecuteNonQuery();

    // 2. DataTable을 임시 테이블에 복사
    using (SqlBulkCopy bulk = new SqlBulkCopy(conn))
    {
        bulk.DestinationTableName = "#Temp";
        bulk.ColumnMappings.Add("RUSIACODE_CODE", "RUSIACODE_CODE");
        bulk.ColumnMappings.Add("COL2", "COL2");
        bulk.WriteToServer(dt);
    }

    // 3. MERGE 구문으로 UPDATE 또는 INSERT
    string merge = @"
        MERGE INTO dbo.RUSIACODE_TEST AS target
        USING #Temp AS source
        ON target.RUSIACODE_CODE = source.RUSIACODE_CODE
        WHEN MATCHED THEN
            UPDATE SET target.COL2 = source.COL2
        WHEN NOT MATCHED THEN
            INSERT (RUSIACODE_CODE, COL2)
            VALUES (source.RUSIACODE_CODE, source.COL2);";

    new SqlCommand(merge, conn).ExecuteNonQuery();
}
