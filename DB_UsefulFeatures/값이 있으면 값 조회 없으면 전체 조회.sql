USE [RUTEC_BASIC]
GO
/****** Object:  StoredProcedure [dbo].[SP_FORM_XLOG]    Script Date: 2025-05-20 오전 10:04:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------
-- SP명		:	SP_FORM_XLOG
-- 작성자	:	박상원
-- 작성일	:   2025-04-17
-- 수정자	:
-- 수정일	:
-- 설명		:	사용자의 행동 추적 관리 프로시저
-- 사용처	:	관리프로그램-기준정보관리-이벤트 발생현황
-- 테이블	:	XLog(사용자 로그)
-- 비고		:
-- HISTORY	:	2025.04.17 박상원 최초작성
--				
--				
--				
--				
--				
--				
---------------------------------------------------------------------------------------------
ALTER      procedure [dbo].[SP_FORM_XLOG](
	@GUBUN					NVARCHAR(50)					  ,					-- CRUD 구분자
	@START_DATE				datetime					= NULL,					-- 조회 시작일자
	@END_DATE				datetime					= NULL,					-- 조회 마지막일자
	@USER_ID				NVARCHAR(20)				=	'',					-- 사용자 ID
	@USER_IP				NVARCHAR(50)				=	'',					-- 사용자 IP
	@ACT_NAME				NVARCHAR(400)				=	'',					-- 이벤트명

	@MESSAGE				NVARCHAR(1000)				OUTPUT					-- Return 메시지
)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		IF(@GUBUN = 'S')	-- XLog 테이블 조회(S)
		BEGIN
			SELECT	[NO]						-- 생성번호
					,[USER_ID]					-- 사용자ID
					,[USER_IP]					-- 사용자IP
					,[ACT_NAME]					-- 이벤트명
					,[ACT_PARAMETER]			-- 이벤트파라미터
					,[LOG_DATE]					-- 로그일자

			FROM	XLOG with(nolock)
	
			WHERE	LOG_DATE BETWEEN @START_DATE AND @END_DATE					-- 조회 기간
				AND (USER_ID LIKE '%' + @USER_ID  + '%' OR @USER_ID = '')		-- USER_ID가 null이거나 공백이면 전체조회 파라미터 값이 있으면 조건 조회
				AND (USER_IP LIKE '%' + @USER_IP  + '%' OR @USER_IP = '')						-- USER_IP가 null이거나 공백이면 전체조회 파라미터 값이 있으면 조건 조회
				AND (ACT_NAME LIKE '%' + @ACT_NAME  + '%' OR @ACT_NAME = '')					-- ACT_NAME이 null이거나 공백이면 전체조회 파라미터 값이 있으면 조건 조회

			SET @MESSAGE = '조회되었습니다'
		END
		ELSE IF(@GUBUN = 'S2')	--조회(S2)
		BEGIN	
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
