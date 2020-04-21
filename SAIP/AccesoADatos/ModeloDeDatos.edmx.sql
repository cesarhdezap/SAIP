
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/21/2020 17:02:25
-- Generated from EDMX file: C:\Users\CETDT\Desktop\repos\SAIP\SAIP\AccesoADatos\ModeloDeDatos.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SAIPDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AlimentoIngredienteIngrediente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlatilloIngrediente] DROP CONSTRAINT [FK_AlimentoIngredienteIngrediente];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoPedido]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pedidos] DROP CONSTRAINT [FK_EmpleadoPedido];
GO
IF OBJECT_ID(N'[dbo].[FK_PedidoAlimentoPedido]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlatilloPedido] DROP CONSTRAINT [FK_PedidoAlimentoPedido];
GO
IF OBJECT_ID(N'[dbo].[FK_DireccionesCliente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Direcciones] DROP CONSTRAINT [FK_DireccionesCliente];
GO
IF OBJECT_ID(N'[dbo].[FK_AlimentoAlimentoPedido]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlatilloPedido] DROP CONSTRAINT [FK_AlimentoAlimentoPedido];
GO
IF OBJECT_ID(N'[dbo].[FK_AlimentoAlimentoIngrediente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlatilloIngrediente] DROP CONSTRAINT [FK_AlimentoAlimentoIngrediente];
GO
IF OBJECT_ID(N'[dbo].[FK_PedidoProductoPedido]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductoPedido] DROP CONSTRAINT [FK_PedidoProductoPedido];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductoPedidoProducto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductoPedido] DROP CONSTRAINT [FK_ProductoPedidoProducto];
GO
IF OBJECT_ID(N'[dbo].[FK_MesaCuenta]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mesas] DROP CONSTRAINT [FK_MesaCuenta];
GO
IF OBJECT_ID(N'[dbo].[FK_IngredienteIngredienteIngrediente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IngredienteIngrediente] DROP CONSTRAINT [FK_IngredienteIngredienteIngrediente];
GO
IF OBJECT_ID(N'[dbo].[FK_IngredienteIngredienteIngrediente1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IngredienteIngrediente] DROP CONSTRAINT [FK_IngredienteIngredienteIngrediente1];
GO
IF OBJECT_ID(N'[dbo].[FK_CuentaPedido]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pedidos] DROP CONSTRAINT [FK_CuentaPedido];
GO
IF OBJECT_ID(N'[dbo].[FK_CuentaCliente_Cuenta]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CuentaCliente] DROP CONSTRAINT [FK_CuentaCliente_Cuenta];
GO
IF OBJECT_ID(N'[dbo].[FK_CuentaCliente_Cliente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CuentaCliente] DROP CONSTRAINT [FK_CuentaCliente_Cliente];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Ingredientes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ingredientes];
GO
IF OBJECT_ID(N'[dbo].[Mesas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mesas];
GO
IF OBJECT_ID(N'[dbo].[Pedidos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pedidos];
GO
IF OBJECT_ID(N'[dbo].[Clientes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clientes];
GO
IF OBJECT_ID(N'[dbo].[Direcciones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Direcciones];
GO
IF OBJECT_ID(N'[dbo].[Platillos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Platillos];
GO
IF OBJECT_ID(N'[dbo].[PlatilloPedido]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlatilloPedido];
GO
IF OBJECT_ID(N'[dbo].[PlatilloIngrediente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlatilloIngrediente];
GO
IF OBJECT_ID(N'[dbo].[Empleados]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Empleados];
GO
IF OBJECT_ID(N'[dbo].[Productos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Productos];
GO
IF OBJECT_ID(N'[dbo].[ProductoPedido]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductoPedido];
GO
IF OBJECT_ID(N'[dbo].[Ivas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ivas];
GO
IF OBJECT_ID(N'[dbo].[Cuentas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cuentas];
GO
IF OBJECT_ID(N'[dbo].[IngredienteIngrediente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IngredienteIngrediente];
GO
IF OBJECT_ID(N'[dbo].[CuentaCliente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CuentaCliente];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Ingredientes'
CREATE TABLE [dbo].[Ingredientes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UnidadDeMedida] smallint  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [CantidadEnInventario] float  NOT NULL,
    [CodigoDeBarras] nvarchar(max)  NOT NULL,
    [Costo] float  NOT NULL,
    [FechaDeCreacion] datetime  NOT NULL,
    [FechaDeModiciacion] datetime  NOT NULL,
    [NombreCreador] nvarchar(max)  NOT NULL,
    [Activo] bit  NOT NULL,
    [Codigo] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Mesas'
CREATE TABLE [dbo].[Mesas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Estado] smallint  NOT NULL
);
GO

-- Creating table 'Pedidos'
CREATE TABLE [dbo].[Pedidos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FechaDeCreacion] datetime  NOT NULL,
    [PrecioTotal] float  NOT NULL,
    [Iva] float  NOT NULL,
    [Estado] smallint  NOT NULL,
    [Empleado_Id] int  NOT NULL,
    [Cuenta_Id] int  NOT NULL
);
GO

-- Creating table 'Clientes'
CREATE TABLE [dbo].[Clientes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Telefono] nvarchar(max)  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [FechaDeCreacion] datetime  NOT NULL,
    [FechaDeModicacion] datetime  NOT NULL,
    [NombreCreador] nvarchar(max)  NOT NULL,
    [Comentarios] nvarchar(max)  NOT NULL,
    [Activo] bit  NOT NULL
);
GO

-- Creating table 'Direcciones'
CREATE TABLE [dbo].[Direcciones] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Direccion] nvarchar(max)  NOT NULL,
    [Clientes_Id] int  NOT NULL
);
GO

-- Creating table 'Platillos'
CREATE TABLE [dbo].[Platillos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Precio] float  NOT NULL,
    [FechaDeCreacion] datetime  NOT NULL,
    [FechaDeModificacion] datetime  NOT NULL,
    [Activo] bit  NOT NULL,
    [Codigo] nvarchar(max)  NOT NULL,
    [Notas] nvarchar(max)  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PlatilloPedido'
CREATE TABLE [dbo].[PlatilloPedido] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Cantidad] int  NOT NULL,
    [Pedido_Id] int  NOT NULL,
    [Alimento_Id] int  NOT NULL
);
GO

-- Creating table 'PlatilloIngrediente'
CREATE TABLE [dbo].[PlatilloIngrediente] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Cantidad] float  NOT NULL,
    [Ingredientes_Id] int  NOT NULL,
    [Alimento_Id] int  NOT NULL
);
GO

-- Creating table 'Empleados'
CREATE TABLE [dbo].[Empleados] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Contraseña] nvarchar(max)  NOT NULL,
    [NombreDeUsuario] nvarchar(max)  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [FechaDeCreacion] datetime  NOT NULL,
    [FechaDeModicacion] datetime  NOT NULL,
    [NombreCreador] nvarchar(max)  NOT NULL,
    [Activo] bit  NOT NULL,
    [TipoDeEmpleado] smallint  NOT NULL
);
GO

-- Creating table 'Productos'
CREATE TABLE [dbo].[Productos] (
    [CodigoDeBarras] nvarchar(max)  NOT NULL,
    [Precio] float  NOT NULL,
    [Id] int  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [CantidadEnInventario] int  NOT NULL,
    [Codigo] nvarchar(max)  NOT NULL,
    [Costo] float  NOT NULL,
    [FechaDeCreacion] datetime  NOT NULL,
    [FechaDeModificacion] datetime  NOT NULL,
    [NombreCreador] nvarchar(max)  NOT NULL,
    [Activo] bit  NOT NULL
);
GO

-- Creating table 'ProductoPedido'
CREATE TABLE [dbo].[ProductoPedido] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Cantidad] int  NOT NULL,
    [Pedido_Id] int  NOT NULL,
    [Productos_Id] int  NOT NULL
);
GO

-- Creating table 'Ivas'
CREATE TABLE [dbo].[Ivas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FechaDeInicio] datetime  NOT NULL,
    [Valor] float  NOT NULL,
    [Creador] nvarchar(max)  NOT NULL,
    [FechaDeCreacion] datetime  NOT NULL,
    [Activo] bit  NOT NULL
);
GO

-- Creating table 'Cuentas'
CREATE TABLE [dbo].[Cuentas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PrecioTotal] float  NOT NULL,
    [Estado] smallint  NOT NULL,
    [Mesa_Id] int  NOT NULL
);
GO

-- Creating table 'IngredienteIngrediente'
CREATE TABLE [dbo].[IngredienteIngrediente] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Cantidad] float  NOT NULL,
    [IngredienteCompuesto_Id] int  NOT NULL,
    [IngredienteComponente_Id] int  NOT NULL
);
GO

-- Creating table 'CuentaCliente'
CREATE TABLE [dbo].[CuentaCliente] (
    [Cuenta_Id] int  NOT NULL,
    [Clientes_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Ingredientes'
ALTER TABLE [dbo].[Ingredientes]
ADD CONSTRAINT [PK_Ingredientes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Mesas'
ALTER TABLE [dbo].[Mesas]
ADD CONSTRAINT [PK_Mesas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pedidos'
ALTER TABLE [dbo].[Pedidos]
ADD CONSTRAINT [PK_Pedidos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Clientes'
ALTER TABLE [dbo].[Clientes]
ADD CONSTRAINT [PK_Clientes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Direcciones'
ALTER TABLE [dbo].[Direcciones]
ADD CONSTRAINT [PK_Direcciones]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Platillos'
ALTER TABLE [dbo].[Platillos]
ADD CONSTRAINT [PK_Platillos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PlatilloPedido'
ALTER TABLE [dbo].[PlatilloPedido]
ADD CONSTRAINT [PK_PlatilloPedido]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PlatilloIngrediente'
ALTER TABLE [dbo].[PlatilloIngrediente]
ADD CONSTRAINT [PK_PlatilloIngrediente]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Empleados'
ALTER TABLE [dbo].[Empleados]
ADD CONSTRAINT [PK_Empleados]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Productos'
ALTER TABLE [dbo].[Productos]
ADD CONSTRAINT [PK_Productos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductoPedido'
ALTER TABLE [dbo].[ProductoPedido]
ADD CONSTRAINT [PK_ProductoPedido]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Ivas'
ALTER TABLE [dbo].[Ivas]
ADD CONSTRAINT [PK_Ivas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cuentas'
ALTER TABLE [dbo].[Cuentas]
ADD CONSTRAINT [PK_Cuentas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IngredienteIngrediente'
ALTER TABLE [dbo].[IngredienteIngrediente]
ADD CONSTRAINT [PK_IngredienteIngrediente]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Cuenta_Id], [Clientes_Id] in table 'CuentaCliente'
ALTER TABLE [dbo].[CuentaCliente]
ADD CONSTRAINT [PK_CuentaCliente]
    PRIMARY KEY CLUSTERED ([Cuenta_Id], [Clientes_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Ingredientes_Id] in table 'PlatilloIngrediente'
ALTER TABLE [dbo].[PlatilloIngrediente]
ADD CONSTRAINT [FK_AlimentoIngredienteIngrediente]
    FOREIGN KEY ([Ingredientes_Id])
    REFERENCES [dbo].[Ingredientes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlimentoIngredienteIngrediente'
CREATE INDEX [IX_FK_AlimentoIngredienteIngrediente]
ON [dbo].[PlatilloIngrediente]
    ([Ingredientes_Id]);
GO

-- Creating foreign key on [Empleado_Id] in table 'Pedidos'
ALTER TABLE [dbo].[Pedidos]
ADD CONSTRAINT [FK_EmpleadoPedido]
    FOREIGN KEY ([Empleado_Id])
    REFERENCES [dbo].[Empleados]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoPedido'
CREATE INDEX [IX_FK_EmpleadoPedido]
ON [dbo].[Pedidos]
    ([Empleado_Id]);
GO

-- Creating foreign key on [Pedido_Id] in table 'PlatilloPedido'
ALTER TABLE [dbo].[PlatilloPedido]
ADD CONSTRAINT [FK_PedidoAlimentoPedido]
    FOREIGN KEY ([Pedido_Id])
    REFERENCES [dbo].[Pedidos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PedidoAlimentoPedido'
CREATE INDEX [IX_FK_PedidoAlimentoPedido]
ON [dbo].[PlatilloPedido]
    ([Pedido_Id]);
GO

-- Creating foreign key on [Clientes_Id] in table 'Direcciones'
ALTER TABLE [dbo].[Direcciones]
ADD CONSTRAINT [FK_DireccionesCliente]
    FOREIGN KEY ([Clientes_Id])
    REFERENCES [dbo].[Clientes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DireccionesCliente'
CREATE INDEX [IX_FK_DireccionesCliente]
ON [dbo].[Direcciones]
    ([Clientes_Id]);
GO

-- Creating foreign key on [Alimento_Id] in table 'PlatilloPedido'
ALTER TABLE [dbo].[PlatilloPedido]
ADD CONSTRAINT [FK_AlimentoAlimentoPedido]
    FOREIGN KEY ([Alimento_Id])
    REFERENCES [dbo].[Platillos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlimentoAlimentoPedido'
CREATE INDEX [IX_FK_AlimentoAlimentoPedido]
ON [dbo].[PlatilloPedido]
    ([Alimento_Id]);
GO

-- Creating foreign key on [Alimento_Id] in table 'PlatilloIngrediente'
ALTER TABLE [dbo].[PlatilloIngrediente]
ADD CONSTRAINT [FK_AlimentoAlimentoIngrediente]
    FOREIGN KEY ([Alimento_Id])
    REFERENCES [dbo].[Platillos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlimentoAlimentoIngrediente'
CREATE INDEX [IX_FK_AlimentoAlimentoIngrediente]
ON [dbo].[PlatilloIngrediente]
    ([Alimento_Id]);
GO

-- Creating foreign key on [Pedido_Id] in table 'ProductoPedido'
ALTER TABLE [dbo].[ProductoPedido]
ADD CONSTRAINT [FK_PedidoProductoPedido]
    FOREIGN KEY ([Pedido_Id])
    REFERENCES [dbo].[Pedidos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PedidoProductoPedido'
CREATE INDEX [IX_FK_PedidoProductoPedido]
ON [dbo].[ProductoPedido]
    ([Pedido_Id]);
GO

-- Creating foreign key on [Productos_Id] in table 'ProductoPedido'
ALTER TABLE [dbo].[ProductoPedido]
ADD CONSTRAINT [FK_ProductoPedidoProducto]
    FOREIGN KEY ([Productos_Id])
    REFERENCES [dbo].[Productos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductoPedidoProducto'
CREATE INDEX [IX_FK_ProductoPedidoProducto]
ON [dbo].[ProductoPedido]
    ([Productos_Id]);
GO

-- Creating foreign key on [Mesa_Id] in table 'Cuentas'
ALTER TABLE [dbo].[Cuentas]
ADD CONSTRAINT [FK_MesaCuenta]
    FOREIGN KEY ([Mesa_Id])
    REFERENCES [dbo].[Mesas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MesaCuenta'
CREATE INDEX [IX_FK_MesaCuenta]
ON [dbo].[Cuentas]
    ([Mesa_Id]);
GO

-- Creating foreign key on [IngredienteCompuesto_Id] in table 'IngredienteIngrediente'
ALTER TABLE [dbo].[IngredienteIngrediente]
ADD CONSTRAINT [FK_IngredienteIngredienteIngrediente]
    FOREIGN KEY ([IngredienteCompuesto_Id])
    REFERENCES [dbo].[Ingredientes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IngredienteIngredienteIngrediente'
CREATE INDEX [IX_FK_IngredienteIngredienteIngrediente]
ON [dbo].[IngredienteIngrediente]
    ([IngredienteCompuesto_Id]);
GO

-- Creating foreign key on [IngredienteComponente_Id] in table 'IngredienteIngrediente'
ALTER TABLE [dbo].[IngredienteIngrediente]
ADD CONSTRAINT [FK_IngredienteIngredienteIngrediente1]
    FOREIGN KEY ([IngredienteComponente_Id])
    REFERENCES [dbo].[Ingredientes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IngredienteIngredienteIngrediente1'
CREATE INDEX [IX_FK_IngredienteIngredienteIngrediente1]
ON [dbo].[IngredienteIngrediente]
    ([IngredienteComponente_Id]);
GO

-- Creating foreign key on [Cuenta_Id] in table 'Pedidos'
ALTER TABLE [dbo].[Pedidos]
ADD CONSTRAINT [FK_CuentaPedido]
    FOREIGN KEY ([Cuenta_Id])
    REFERENCES [dbo].[Cuentas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CuentaPedido'
CREATE INDEX [IX_FK_CuentaPedido]
ON [dbo].[Pedidos]
    ([Cuenta_Id]);
GO

-- Creating foreign key on [Cuenta_Id] in table 'CuentaCliente'
ALTER TABLE [dbo].[CuentaCliente]
ADD CONSTRAINT [FK_CuentaCliente_Cuenta]
    FOREIGN KEY ([Cuenta_Id])
    REFERENCES [dbo].[Cuentas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Clientes_Id] in table 'CuentaCliente'
ALTER TABLE [dbo].[CuentaCliente]
ADD CONSTRAINT [FK_CuentaCliente_Cliente]
    FOREIGN KEY ([Clientes_Id])
    REFERENCES [dbo].[Clientes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CuentaCliente_Cliente'
CREATE INDEX [IX_FK_CuentaCliente_Cliente]
ON [dbo].[CuentaCliente]
    ([Clientes_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------