DECLARE @idDepresion as int = (Select Top 1 Id from ConfiguracionesTest where Nombre='Depresi�n')

insert into [dbo].[ConfiguracionesSecciones]
(Seccion,NumeroPreguntas,IdConfiguracionesTest,FormulaAgregado,Creado,Eliminado)
Values
('Depresi�n','21',@idDepresion,'SUM',getdate(),0)
SELECT *  FROM [dbo].[ConfiguracionesSecciones]

