Insert into [dbo].[ConfiguracionesSecciones] ([Seccion],[NumeroPreguntas],[IdConfiguracionesTest],[FormulaAgregado],[Creado],[Eliminado]) values('Extroversión',5,(select top 1 id from [dbo].[ConfiguracionesTest] where [Nombre] like '%Personalidad%'  ),'AVG',getdate(),0)
Insert into [dbo].[ConfiguracionesSecciones] ([Seccion],[NumeroPreguntas],[IdConfiguracionesTest],[FormulaAgregado],[Creado],[Eliminado]) values('Responsabilidad',5,(select top 1 id from [dbo].[ConfiguracionesTest] where [Nombre] like '%Personalidad%'  ),'AVG',getdate(),0)
Insert into [dbo].[ConfiguracionesSecciones] ([Seccion],[NumeroPreguntas],[IdConfiguracionesTest],[FormulaAgregado],[Creado],[Eliminado]) values('Amabilidad ',5,(select top 1 id from [dbo].[ConfiguracionesTest] where [Nombre] like '%Personalidad%'  ),'AVG',getdate(),0)
Insert into [dbo].[ConfiguracionesSecciones] ([Seccion],[NumeroPreguntas],[IdConfiguracionesTest],[FormulaAgregado],[Creado],[Eliminado]) values('Neuroticismo ',5,(select top 1 id from [dbo].[ConfiguracionesTest] where [Nombre] like '%Personalidad%'  ),'AVG',getdate(),0)
Insert into [dbo].[ConfiguracionesSecciones] ([Seccion],[NumeroPreguntas],[IdConfiguracionesTest],[FormulaAgregado],[Creado],[Eliminado]) values('Apertura',5,(select top 1 id from [dbo].[ConfiguracionesTest] where [Nombre] like '%Personalidad%'  ),'AVG',getdate(),0)

select * from  [dbo].[ConfiguracionesSecciones]

