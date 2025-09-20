DECLARE @idAnsiedad as int = (Select Top 1 Id from ConfiguracionesTest where Nombre='Ansiedad')
DECLARE @idSeccionAnsiedad as int = (Select Top 1 Id from ConfiguracionesSecciones where IdConfiguracionesTest=@idAnsiedad)


INSERT INTO [dbo].[ConfiguracionesOpciones]
           ([Orden]
           ,[Opcion]
           ,[Peso]
           ,[IdConfiguracionPreguntas]
           ,[Creado]
           ,[Eliminado])
		   
SELECT 1 Orden,
	'No en absoluto' Opcion,
	case 
	   when [Inversa]=0 then  0 else 3 end as Peso,
	[Id] IdPregunta,
	GETDATE() Creado,
	0 Eliminado
		
FROM [dbo].[ConfiguracionesPreguntas]
where IdConfiguracionSecciones=@idSeccionAnsiedad
UNION ALL
SELECT  2 Orden,
	'Levemente' Opcion,
	case 
	   when [Inversa]=0 then  1 else 2 end as Peso,
	   [Id] IdPregunta,
	   GETDATE() Creado,
	   0 Eliminado
	
FROM [PI-5toGrupoApi].[dbo].[ConfiguracionesPreguntas]
where IdConfiguracionSecciones=@idSeccionAnsiedad
union all
SELECT  3 Orden,
	'Moderadamente' Opcion,
	case 
	   when [Inversa]=0 then  2 else 1 end as Peso,
	   [Id] IdPregunta,
	   GETDATE() Creado,
	   0 Eliminado
	
FROM [PI-5toGrupoApi].[dbo].[ConfiguracionesPreguntas]
where IdConfiguracionSecciones=@idSeccionAnsiedad
union all
SELECT  4 Orden,
	'Severamente' Opcion,
	case 
	   when [Inversa]=0 then  3 else 0 end as Peso,
	   [Id] IdPregunta,
	   GETDATE() Creado,
	   0 Eliminado
	
FROM [PI-5toGrupoApi].[dbo].[ConfiguracionesPreguntas]
where IdConfiguracionSecciones=@idSeccionAnsiedad
order by IdPregunta,Orden

go 

select * from [dbo].[ConfiguracionesOpciones]
