DECLARE @idAutoestima as int = (Select Top 1 Id from ConfiguracionesTest where Nombre='Ansiedad')

insert into [dbo].[ConfiguracionesSecciones]
(Seccion,NumeroPreguntas,IdConfiguracionesTest,FormulaAgregado,Creado,Eliminado)
Values
('Ansiedad','21',@idAutoestima,'SUM',getdate(),0)
SELECT *  FROM [dbo].[ConfiguracionesSecciones]

