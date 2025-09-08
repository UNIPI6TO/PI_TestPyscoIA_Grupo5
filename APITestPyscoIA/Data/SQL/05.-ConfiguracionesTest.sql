Insert into [dbo].[ConfiguracionesTest]([Nombre],[Duracion],[IdTipoTest],[Creado],[Eliminado]) values('Personalidad',1200,(select top 1 id from [dbo].[TipoTest] where Nombre like '%Personalidad%'),getdate(),0)
Insert into [dbo].[ConfiguracionesTest]([Nombre],[Duracion],[IdTipoTest],[Creado],[Eliminado]) values('Autoestima',600,(select top 1 id from [dbo].[TipoTest] where Nombre like '%Autoestima%'),getdate(),0)

select * from [dbo].[ConfiguracionesTest]