GO

INSERT INTO [dbo].[Usuario]
           ([Usuario]
           ,[Password]
           ,[Rol]
           ,[Creado]
           ,[Eliminado])
     VALUES
           ('admin'
           ,'admin'
           ,'ADMIN'
           ,getdate()
           ,0)
GO


select * from [dbo].[Usuario]