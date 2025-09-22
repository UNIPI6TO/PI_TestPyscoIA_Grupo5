
Insert into [dbo].[ConfiguracionesTest]([Nombre],[Duracion],[IdTipoTest],[Creado],[Eliminado]) values('Ansiedad',1200,(select top 1 id from [dbo].[TipoTest] where Nombre like '%Ansiedad%'),getdate(),0)

select * from [dbo].[ConfiguracionesTest]