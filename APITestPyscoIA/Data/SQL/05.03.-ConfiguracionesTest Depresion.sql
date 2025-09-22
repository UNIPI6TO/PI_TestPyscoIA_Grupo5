
Insert into [dbo].[ConfiguracionesTest]([Nombre],[Duracion],[IdTipoTest],[Creado],[Eliminado]) values('Depresión',1200,(select top 1 id from [dbo].[TipoTest] where Nombre like '%Depresión%'),getdate(),0)

select * from [dbo].[ConfiguracionesTest]