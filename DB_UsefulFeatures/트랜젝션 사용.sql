BEGIN TRAN;

-- 1. 어떤 작업들 (INSERT, UPDATE 등)
UPDATE 테이블명
SET 컬럼 = '값'
WHERE 조건;

-- 2. 에러 체크 및 커밋/롤백
IF @@ERROR <> 0
BEGIN
    ROLLBACK;
    PRINT '에러 발생하여 롤백함';
END
ELSE
BEGIN
    COMMIT;
    PRINT '성공적으로 커밋함';
END
