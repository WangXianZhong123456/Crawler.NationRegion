--ע:������Щ��������ֱ�Ӿ͵���,������⴦��,��������


-- 1��ִ���������
INSERT INTO UCML_NationRegionExpetion 
([Uri],[Level],[IsUpdate],[CreateTime],[ModifyTime])
select 'http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/2016'+'/'+ a.href+'.html',4,0,GETDATE(),GETDATE()
from UCML_NationRegion a where Level=2
and not exists(select 1 from UCML_NationRegion where ParentNode=a.node)

-- 2����Ӧ�ó�������->�쳣���ݴ���ִ�����,�ر�Ӧ�ó������ߡ�������ʡ/��/����/��/�塿,�������ִ���������
UPDATE UCML_NationRegionExpetion
SET URI = CASE WHEN LEVEL =5 AND LEN(uri) = 80 THEN  SUBSTRING(uri,0,64)+SUBSTRING(uri,67,LEN(uri))ELSE URI END 
WHERE ISUPDATE = 0 

-- 3���ڶ�������ִ������ٵ���쳣����ִ�о�OK��
