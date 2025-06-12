DECLARE @TableName NVARCHAR(100) = 'BCDPRODUCTINFO';
	SELECT 
		ROW_NUMBER() OVER (ORDER BY c.column_id) AS [번호 (No)],
		c.name AS [열 이름 (Column Name)],
		t.name AS [데이터형식 (Type)],
		CASE 
			WHEN t.name IN ('nvarchar','varchar','char','nchar') THEN
				CASE 
					WHEN c.max_length = -1 THEN 'MAX'
					ELSE CAST(c.max_length / CASE WHEN t.name LIKE 'n%' THEN 2 ELSE 1 END AS VARCHAR)
				END
			WHEN t.name IN ('decimal','numeric')
				THEN CAST(c.precision AS VARCHAR) + ',' + CAST(c.scale AS VARCHAR)
			ELSE ''
		END AS [길이 (Length)],
		CASE WHEN c.is_nullable = 1 THEN 'NULL' ELSE 'NOT NULL' END AS [Null 허용 (Nullable)],
		ISNULL(
			(
				SELECT STRING_AGG(
					CASE 
						WHEN i.is_primary_key = 1 THEN 'PK'
						ELSE 'Index'
					END, ', '
				)
				FROM sys.index_columns ic
				INNER JOIN sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id
				WHERE ic.object_id = c.object_id
					AND ic.column_id = c.column_id
			)
		, '') AS [색인 (Index)],
		ISNULL(ep.value, '') AS [명칭 (Caption)],
		'' AS [비고 (Remark)]
	FROM sys.columns c
	JOIN sys.types t ON c.user_type_id = t.user_type_id
	LEFT JOIN sys.extended_properties ep
		ON ep.major_id = c.object_id 
		AND ep.minor_id = c.column_id 
		AND ep.name = 'MS_Description'
	WHERE c.object_id = OBJECT_ID(@TableName)
	ORDER BY c.column_id
