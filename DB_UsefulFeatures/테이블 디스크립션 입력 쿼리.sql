EXEC sp_addextendedproperty 
    @name = N'MS_Description',                -- 속성 이름 (표준 Description)
    @value = N'수정자ID',  -- 실제 설명(한글/영문 가능)
    @level0type = N'SCHEMA', @level0name = N'dbo',         -- 스키마명
    @level1type = N'TABLE',  @level1name = N'BCDPRODUCTINFO',    -- 테이블명
    @level2type = N'COLUMN', @level2name = N'LAST_UPDATED_BY';     -- 컬럼명
