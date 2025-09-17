DECLARE @idAutoestima as int = (Select Top 1 Id from ConfiguracionesTest where Nombre='Autoestima')

insert into [dbo].[ConfiguracionesSecciones]
(Seccion,NumeroPreguntas,IdConfiguracionesTest,FormulaAgregado,Creado,Eliminado)
Values
('Autoestima','10',@idAutoestima,'SUM',getdate(),0)
SELECT *  FROM [dbo].[ConfiguracionesSecciones]

