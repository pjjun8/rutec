USE [RUTEC_BASIC]
GO
/****** Object:  StoredProcedure [dbo].[SP_FORM_AUDIT_TRAIL]    Script Date: 2025-04-17 오전 11:39:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



---------------------------------------------------------------------------------------------
-- SP명		:	SP_FORM_AUDIT_TRAIL
-- 작성자	:	박상원
-- 작성일	:   2025-04-16
-- 수정자	:
-- 수정일	:
-- 설명		:	테이블 기록 추적 관리 프로시저
-- 사용처	:	관리프로그램-기준정보관리-AuditTrail
-- 테이블	:	모든 테이블
-- 비고		:
-- HISTORY	:	2025.04.16 박상원 최초작성
--				
--				
--				
--				
--				
--				
---------------------------------------------------------------------------------------------
ALTER procedure [dbo].[SP_FORM_AUDIT_TRAIL](
	@GUBUN					NVARCHAR(50)				  ,	-- CRUD 구분자
	@TABLE_NAME				NVARCHAR(100)	=			'',	-- 조회할 테이블 이름 파라미터

	@MESSAGE				NVARCHAR(1000)			OUTPUT	-- 메시지
)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		IF(@GUBUN = 'S')	-- DB의 모든 테이블명 조회(S)
		BEGIN

		SELECT TABLE_NAME
		FROM INFORMATION_SCHEMA.TABLES
		WHERE TABLE_TYPE = 'BASE TABLE'

			SET @MESSAGE = '조회되었습니다'
		END
		ELSE IF(@GUBUN = 'S2')	-- 선택된 테이블 조회(S2)
		BEGIN

		DECLARE @SQL NVARCHAR(MAX)							-- sp_executesql을 이용하기위한 파라미터 생성
		SET @SQL = N'SELECT * FROM ' + @TABLE_NAME			-- sp_executesql에 넘겨주기위한 파라미터 값 입력
		EXEC sp_executesql @SQL								-- sp_executesql을 이용한 동적 SQL 사용 -> 입력된 테이블 조회

			SET @MESSAGE = '조회되었습니다'
		END
		ELSE IF(@GUBUN = 'U')	--수정(U2,U3,U4)
		BEGIN	
			SET @MESSAGE = '수정되었습니다'
		END
		ELSE IF (@GUBUN = 'D')	--삭제(D2,D3,D4)
        BEGIN        
            SET @MESSAGE = '삭제되었습니다.'
        END
		ELSE
		BEGIN
			SET @MESSAGE = '알 수 없는 처리 구분입니다.'
		END
	END TRY
	BEGIN CATCH
        SET @MESSAGE = '처리 중 오류 발생: ' + ERROR_MESSAGE()
    END CATCH
END
