
Insert into [dbo].[ConfiguracionesTest]([Nombre],[Duracion],[IdTipoTest],[Creado],[Eliminado]) values('Depresi�n',1200,(select top 1 id from [dbo].[TipoTest] where Nombre like '%Depresi�n%'),getdate(),0)

select * from [dbo].[ConfiguracionesTest]