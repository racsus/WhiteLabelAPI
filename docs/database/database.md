# Database architecture

## NonConformities Global Tables 📋

#### NonConformities

```
CREATE TABLE [dbo].[NoConformidades]
(
    [Id] INT NOT NULL IDENTITY,
    [IdNoConformidad] NVARCHAR(50) NOT NULL , 
    [ResponsableNC] INT,
    [IdEstado] INT NULL,
    [IdTipo] INT NULL, 
    [Coste] DECIMAL null DEFAULT 0,
    [TipoCambio] nvarchar(3) NOT NULL DEFAULT 'EUR',
    [IdCentroProduccion] INT NOT NULL, 
    [IdDepartamento] INT NULL, 
    [DetectadaPor] INT,
    [IdCategoria] INT NULL, 
    [IdOrigen] INT NULL,
    [IdCausa] INT NULL,
    [Titulo] NVARCHAR(250) NULL  , 
    [Detalles] NTEXT NULL, 
    [Evidencias] NTEXT NULL,
    [AccionInmediata] NTEXT NULL, 
    [AnalisisRaizNC] NTEXT NULL, 
    [ComentarioCorregidoEfectivamente] NTEXT NULL,
    [ComentarioAccionCorrectiva] NTEXT NULL,
    [CorregidaEfectivamente] NVARCHAR(5) NULL DEFAULT '0' ,
    [GeneraAccionCorrectiva] NVARCHAR(5) NULL DEFAULT '0' ,
    [FechaDeteccion] DATE NULL, 
    [FechaCierre] DATE NULL,  
    [FechaCierreEstimado] DATE NULL,
    [FechaModificacion] DATE NULL, 
    [FechaCreacion] DATE NULL,
    [Autor] INT NOT NULL, 
    [UltimoEditor] INT NOT NULL, 
    [RowVersion] ROWVERSION NULL, 
    CONSTRAINT [FK_NoConformidades_CentroProduccion] FOREIGN KEY ([IdCentroProduccion]) REFERENCES [dbo].[CentrosProduccion]([IdCentroProduccion]), 
    CONSTRAINT [FK_NoConformidades_Usuarios_Autor] FOREIGN KEY ([Autor]) REFERENCES [Usuarios]([IdUsuario]), 
    CONSTRAINT [FK_NoConformidades_Usuarios_UltimoEditor] FOREIGN KEY ([UltimoEditor]) REFERENCES [Usuarios]([IdUsuario]), 
    CONSTRAINT [PK_NoConformidades] PRIMARY KEY ([Id]) 
```

#### NonConformitiesDocuments 

```
CREATE TABLE [dbo].[NoConformidadesFotos]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [IdFoto] NVARCHAR(50) NOT NULL
)
```

#### NonConformitiesCorrectiveActions 

```
CREATE TABLE [dbo].[NoConformidadesAccionesCorrectivas]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IdNoConformidad] INT NOT NULL,
    [Accion] NVARCHAR(150) NOT NULL, 
    [Responsable] INT NOT NULL, 
    [FechaPropuesta] DATE NULL, 
    [FechaRegistro] DATETIME NULL, 
    CONSTRAINT [FK_NoConformidadesAccionesCorrectivas_nc] FOREIGN KEY ([IdNoConformidad]) REFERENCES [NoConformidades]([Id])
)

```

#### NonConformitiesActions 
```
CREATE TABLE [dbo].[NoConformidadesAcciones]
(
    [IdAccion] INT NOT NULL PRIMARY KEY, 
    [Nombre] NVARCHAR(50) NULL, 
    [Tipo] INT NULL,
    [IdNoConformidad] INT NULL,
    [IdCentroProduccion] INT NULL, 
    [FechaAccion] date NULL, 
    [Descripcion] NTEXT NULL, 
    [Justificacion] NTEXT NULL,     
    [Responsable] INT NULL, 
    [PlazoFin] INT NULL, 
    [FechaCierre] DATE NULL, 
    [ComprobacionEficacia] NTEXT NULL, 
    [RiesgoResidual] NTEXT NULL, 
    [Coste] DECIMAL NULL, 
    [FechaRegistro] DATETIME NULL DEFAULT GetDate(), 
    [FechaModificcion] DATETIME NULL DEFAULT GetDate(), 
    [Autor] INT NOT NULL, 
    [UltimoEditor] INT NULL, 
    CONSTRAINT [FK_NoConformidadesAcciones_CentrosProduccion] FOREIGN KEY ([IdCentroProduccion]) REFERENCES [CentrosProduccion]([IdCentroProduccion]), 
    CONSTRAINT [FK_NoConformidadesAcciones_NC] FOREIGN KEY ([IdNoConformidad]) REFERENCES [NoConformidades]([Id]),

)
```
   
  
## ForeignKey Tables 📋

#### ProductionCentres 

```
CREATE TABLE [dbo].[CentrosProduccion]
(
    [IdCentroProduccion] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Codigo] NVARCHAR(20) NOT NULL,
    [Nombre] NVARCHAR(50) NOT NULL, 
    [Ubicacion] NVARCHAR(50) NULL, 
    [Color] NVARCHAR(10) NULL, 
    [PlantillaPDF] INT NULL, 
    [TipoCambio] NVARCHAR(3) NULL    
)
```

#### Users 

```
CREATE TABLE [dbo].[Usuarios]
(
    [IdUsuario] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Email] NVARCHAR(100) NOT NULL UNIQUE, 
    [Nombre] NVARCHAR(100) NULL, 
    [IdCentroProduccion] INT NULL, 
    [Departamento] NVARCHAR(50) NULL, 
    [Puesto] NVARCHAR(50) NULL, 
    [Telefono] NVARCHAR(20) NULL, 
    [Extension] NVARCHAR(10) NULL, 
    [Responsable] INT NULL, 
    [Activo] TINYINT NOT NULL DEFAULT 1, 
    [RowVersion] ROWVERSION NOT NULL, 
    [IdCliente] INT NULL, 
    [IdIdioma] INT NULL, 
    CONSTRAINT [FK_Usuarios_Clientes] FOREIGN KEY ([IdCliente]) REFERENCES [Clientes]([IdCliente]), 
    CONSTRAINT [FK_Usuarios_Idiomas] FOREIGN KEY ([IdIdioma]) REFERENCES [Idiomas]([IdIdioma]), 
    CONSTRAINT [FK_Usuarios_CentrosProduccion] FOREIGN KEY ([IdCentroProduccion]) REFERENCES [CentrosProduccion]([IdCentroProduccion])
)
```

#### Departments 

```
CREATE TABLE [dbo].[Departamentos]
(
    [IdDepartamento] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Departamento] NVARCHAR(50) NOT NULL, 
    [Referencia] NVARCHAR(50) NOT NULL
)
```

#### Statuses 

```
CREATE TABLE [dbo].[Estados]
(
    [IdEstado] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nombre] NVARCHAR(250) NOT NULL, 
    [Descripcion] NTEXT NULL, 
    [IdModulo] INT NOT NULL, 
    CONSTRAINT [FK_Estados_Modulos] FOREIGN KEY ([IdModulo]) REFERENCES [Modulos]([IdModulo])
)
```

#### Categories 

```
CREATE TABLE [dbo].[Categorias]
(
    [IdCategoria] INT NOT NULL , 
    [IdIdioma] INT NOT NULL, 
    [IdFormulario] INT NOT NULL, 
    [NombreCategoria] NVARCHAR(50) NOT NULL, 
    [Descripcion] NTEXT NULL, 
    [Identidad] INT NOT NULL IDENTITY, 
    CONSTRAINT [FK_Categorias_Idiomas] FOREIGN KEY ([IdIdioma]) REFERENCES [Idiomas]([IdIdioma]), 
    PRIMARY KEY ([Identidad]), 
    CONSTRAINT [U_KEY] UNIQUE ([IdCategoria],[IdIdioma],[IdFormulario])
)
```

#### Origins 

```
CREATE TABLE [dbo].[Categorias]
(
    [IdCategoria] INT NOT NULL , 
    [IdIdioma] INT NOT NULL, 
    [IdFormulario] INT NOT NULL, 
    [NombreCategoria] NVARCHAR(50) NOT NULL, 
    [Descripcion] NTEXT NULL, 
    [Identidad] INT NOT NULL IDENTITY, 
    CONSTRAINT [FK_Categorias_Idiomas] FOREIGN KEY ([IdIdioma]) REFERENCES [Idiomas]([IdIdioma]), 
    PRIMARY KEY ([Identidad]), 
    CONSTRAINT [U_KEY] UNIQUE ([IdCategoria],[IdIdioma],[IdFormulario])
)
```

#### Causes 

```
CREATE TABLE [dbo].[Categorias]
(
    [IdCategoria] INT NOT NULL , 
    [IdIdioma] INT NOT NULL, 
    [IdFormulario] INT NOT NULL, 
    [NombreCategoria] NVARCHAR(50) NOT NULL, 
    [Descripcion] NTEXT NULL, 
    [Identidad] INT NOT NULL IDENTITY, 
    CONSTRAINT [FK_Categorias_Idiomas] FOREIGN KEY ([IdIdioma]) REFERENCES [Idiomas]([IdIdioma]), 
    PRIMARY KEY ([Identidad]), 
    CONSTRAINT [U_KEY] UNIQUE ([IdCategoria],[IdIdioma],[IdFormulario])
)
```