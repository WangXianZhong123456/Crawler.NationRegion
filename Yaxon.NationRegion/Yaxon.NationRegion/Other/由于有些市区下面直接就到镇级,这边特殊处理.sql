--注:由于有些市区下面直接就到镇级,这边特殊处理,步骤如下


-- 1、执行下面语句
INSERT INTO UCML_NationRegionExpetion 
([Uri],[Level],[IsUpdate],[CreateTime],[ModifyTime])
select 'http://www.stats.gov.cn/tjsj/tjbz/tjyqhdmhcxhfdm/2016'+'/'+ a.href+'.html',4,0,GETDATE(),GETDATE()
from UCML_NationRegion a where Level=2
and not exists(select 1 from UCML_NationRegion where ParentNode=a.node)

-- 2、在应用程序【其他->异常数据处理】执行完后,关闭应用程序再走【正常的省/市/区县/镇/村】,跑完后再执行下面语句
UPDATE UCML_NationRegionExpetion
SET URI = CASE WHEN LEVEL =5 AND LEN(uri) = 80 THEN  SUBSTRING(uri,0,64)+SUBSTRING(uri,67,LEN(uri))ELSE URI END 
WHERE ISUPDATE = 0 

-- 3、第二点的语句执行完后再点击异常数据执行就OK了
