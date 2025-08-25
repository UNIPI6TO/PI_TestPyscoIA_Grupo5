

Declare @IdPais AS INT

SELECT @IdPais = Id
FROM Paises
WHERE Nombre = 'Ecuador'; 


INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Azuay',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Bolívar',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Cañar',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Carchi',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Chimborazo',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Cotopaxi',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'El Oro',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Esmeraldas',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Galápagos',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Guayas',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Imbabura',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Loja',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Los Ríos',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Manabí',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Morona Santiago',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Napo',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Orellana',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Pastaza',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Pichincha',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Santa Elena',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Santo Domingo de los Tsáchilas',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Sucumbíos',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Tungurahua',@IdPais,GETDATE() ,0)
INSERT INTO [dbo].[Provincias]([Nombre],[IdPais],[Creado],[Eliminado]) VALUES ( 'Zamora Chinchipe',@IdPais,GETDATE() ,0)

select * from Provincias