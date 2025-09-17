DECLARE @idAutoestima as int = (Select Top 1 Id from ConfiguracionesTest where Nombre='Autoestima')
DECLARE @idSeccionAutoestima as int = (Select Top 1 Id from ConfiguracionesSecciones where IdConfiguracionesTest=@idAutoestima)


INSERT INTO [dbo].[ConfiguracionesOpciones]
           ([Orden]
           ,[Opcion]
           ,[Peso]
           ,[IdConfiguracionPreguntas]
           ,[Creado]
           ,[Eliminado])
		   
SELECT 1 Orden,
	'Totalmente en desacuerdo' Opcion,
	case 
	   when [Inversa]=0 then  1 else 4 end as Peso,
	[Id] IdPregunta,
	GETDATE() Creado,
	0 Eliminado
		
FROM [dbo].[ConfiguracionesPreguntas]
where IdConfiguracionSecciones=@idSeccionAutoestima
UNION ALL
SELECT  2 Orden,
	'En desacuerdo' Opcion,
	case 
	   when [Inversa]=0 then  2 else 3 end as Peso,
	   [Id] IdPregunta,
	   GETDATE() Creado,
	   0 Eliminado
	
FROM [PI-5toGrupoApi].[dbo].[ConfiguracionesPreguntas]
where IdConfiguracionSecciones=@idSeccionAutoestima
union all
SELECT  4 Orden,
	'De acuerdo' Opcion,
	case 
	   when [Inversa]=0 then  3 else 2 end as Peso,
	   [Id] IdPregunta,
	   GETDATE() Creado,
	   0 Eliminado
	
FROM [PI-5toGrupoApi].[dbo].[ConfiguracionesPreguntas]
where IdConfiguracionSecciones=@idSeccionAutoestima
union all
SELECT  5 Orden,
	'Totalmente de acuerdo' Opcion,
	case 
	   when [Inversa]=0 then  4 else 1 end as Peso,
	   [Id] IdPregunta,
	   GETDATE() Creado,
	   0 Eliminado
	
FROM [PI-5toGrupoApi].[dbo].[ConfiguracionesPreguntas]
where IdConfiguracionSecciones=@idSeccionAutoestima
order by IdPregunta,Orden

go 

select * from [dbo].[ConfiguracionesOpciones]
