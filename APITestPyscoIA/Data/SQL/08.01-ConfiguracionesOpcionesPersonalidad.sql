
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
	   when [Inversa]=0 then  1 else 5 end as Peso,
	[Id] IdPregunta,
	GETDATE() Creado,
	0 Eliminado
		
FROM [PI-5toGrupoApi].[dbo].[ConfiguracionesPreguntas]
UNION ALL
SELECT  2 Orden,
	'En desacuerdo' Opcion,
	case 
	   when [Inversa]=0 then  2 else 4 end as Peso,
	   [Id] IdPregunta,
	   GETDATE() Creado,
	   0 Eliminado
	
FROM [PI-5toGrupoApi].[dbo].[ConfiguracionesPreguntas]
union all
SELECT  3 Orden,
	'Ni de acuerdo ni en desacuerdo' Opcion,
	case 
	   when [Inversa]=0 then  3 else 3 end as Peso,
	   [Id] IdPregunta,
	   GETDATE() Creado,
	   0 Eliminado
	
FROM [PI-5toGrupoApi].[dbo].[ConfiguracionesPreguntas]
union all
SELECT  4 Orden,
	'De acuerdo' Opcion,
	case 
	   when [Inversa]=0 then  4 else 2 end as Peso,
	   [Id] IdPregunta,
	   GETDATE() Creado,
	   0 Eliminado
	
FROM [PI-5toGrupoApi].[dbo].[ConfiguracionesPreguntas]
union all
SELECT  5 Orden,
	'Totalmente de acuerdo' Opcion,
	case 
	   when [Inversa]=0 then  5 else 1 end as Peso,
	   [Id] IdPregunta,
	   GETDATE() Creado,
	   0 Eliminado
	
FROM [PI-5toGrupoApi].[dbo].[ConfiguracionesPreguntas]

order by IdPregunta,Orden

go 

select * from [dbo].[ConfiguracionesOpciones]
