
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/17/2020 01:46:05
-- Generated from EDMX file: C:\Users\marcu\Documents\SAIP\COD\SAIP\AccesoADatos\ModeloDeDatos.edmx
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
    ALTER TABLE [dbo].[AlimentoIngredientes] DROP CONSTRAINT [FK_AlimentoIngredienteIngrediente];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoPedido]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pedidoes] DROP CONSTRAINT [FK_EmpleadoPedido];
GO
IF OBJECT_ID(N'[dbo].[FK_PedidoAlimentoPedido]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AlimentoPedidoes] DROP CONSTRAINT [FK_PedidoAlimentoPedido];
GO
IF OBJECT_ID(N'[dbo].[FK_PedidaADomicilioCliente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pedidoes_PedidoADomicilio] DROP CONSTRAINT [FK_PedidaADomicilioCliente];
GO
IF OBJECT_ID(N'[dbo].[FK_DireccionesCliente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Clientes] DROP CONSTRAINT [FK_DireccionesCliente];
GO
IF OBJECT_ID(N'[dbo].[FK_AlimentoAlimentoPedido]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AlimentoPedidoes] DROP CONSTRAINT [FK_AlimentoAlimentoPedido];
GO
IF OBJECT_ID(N'[dbo].[FK_AlimentoAlimentoIngrediente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AlimentoIngredientes] DROP CONSTRAINT [FK_AlimentoAlimentoIngrediente];
GO
IF OBJECT_ID(N'[dbo].[FK_PedidoProductoPedido]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductoPedidoes] DROP CONSTRAINT [FK_PedidoProductoPedido];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductoPedidoProducto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductoPedidoes] DROP CONSTRAINT [FK_ProductoPedidoProducto];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductoIngrediente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ingredientes] DROP CONSTRAINT [FK_ProductoIngrediente];
GO
IF OBJECT_ID(N'[dbo].[FK_EmpleadoCuenta]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cuentas] DROP CONSTRAINT [FK_EmpleadoCuenta];
GO
IF OBJECT_ID(N'[dbo].[FK_MesaCuenta]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mesas] DROP CONSTRAINT [FK_MesaCuenta];
GO
IF OBJECT_ID(N'[dbo].[FK_CuentaPedidoLocal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pedidoes_PedidoLocal] DROP CONSTRAINT [FK_CuentaPedidoLocal];
GO
IF OBJECT_ID(N'[dbo].[FK_IngredienteIngredienteIngrediente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IngredienteIngredientes] DROP CONSTRAINT [FK_IngredienteIngredienteIngrediente];
GO
IF OBJECT_ID(N'[dbo].[FK_IngredienteIngredienteIngrediente1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IngredienteIngredientes] DROP CONSTRAINT [FK_IngredienteIngredienteIngrediente1];
GO
IF OBJECT_ID(N'[dbo].[FK_PedidoADomicilio_inherits_Pedido]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pedidoes_PedidoADomicilio] DROP CONSTRAINT [FK_PedidoADomicilio_inherits_Pedido];
GO
IF OBJECT_ID(N'[dbo].[FK_PedidoLocal_inherits_Pedido]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pedidoes_PedidoLocal] DROP CONSTRAINT [FK_PedidoLocal_inherits_Pedido];
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
IF OBJECT_ID(N'[dbo].[Pedidoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pedidoes];
GO
IF OBJECT_ID(N'[dbo].[Clientes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clientes];
GO
IF OBJECT_ID(N'[dbo].[Direcciones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Direcciones];
GO
IF OBJECT_ID(N'[dbo].[Alimentoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Alimentoes];
GO
IF OBJECT_ID(N'[dbo].[AlimentoPedidoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AlimentoPedidoes];
GO
IF OBJECT_ID(N'[dbo].[AlimentoIngredientes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AlimentoIngredientes];
GO
IF OBJECT_ID(N'[dbo].[Empleadoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Empleadoes];
GO
IF OBJECT_ID(N'[dbo].[Productoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Productoes];
GO
IF OBJECT_ID(N'[dbo].[ProductoPedidoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductoPedidoes];
GO
IF OBJECT_ID(N'[dbo].[Ivas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ivas];
GO
IF OBJECT_ID(N'[dbo].[Cuentas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cuentas];
GO
IF OBJECT_ID(N'[dbo].[IngredienteIngredientes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IngredienteIngredientes];
GO
IF OBJECT_ID(N'[dbo].[Pedidoes_PedidoADomicilio]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pedidoes_PedidoADomicilio];
GO
IF OBJECT_ID(N'[dbo].[Pedidoes_PedidoLocal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pedidoes_PedidoLocal];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Ingredientes'
CREATE TABLE [dbo].[Ingredientes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UnidadDeMedida] nvarchar(max)  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [CantidadEnInventario] float  NOT NULL,
    [CodigoDeBarras] nvarchar(max)  NOT NULL,
    [Costo] float  NOT NULL,
    [FechaDeCreacion] datetime  NOT NULL,
    [FechaDeModiciacion] datetime  NOT NULL,
    [Creador] nvarchar(max)  NOT NULL,
    [Activo] bit  NOT NULL,
    [Codigo] nvarchar(max)  NOT NULL,
    [Producto_Id] int  NULL
);
GO

-- Creating table 'Mesas'
CREATE TABLE [dbo].[Mesas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Estado] nvarchar(max)  NOT NULL,
    [Cuentas_Id] int  NOT NULL
);
GO

-- Creating table 'Pedidoes'
CREATE TABLE [dbo].[Pedidoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FechaDeCreacion] datetime  NOT NULL,
    [Creador] nvarchar(max)  NOT NULL,
    [PrecioTotal] float  NOT NULL,
    [Iva] float  NOT NULL,
    [Empleado_Id] int  NOT NULL
);
GO

-- Creating table 'Clientes'
CREATE TABLE [dbo].[Clientes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Telefono] nvarchar(max)  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [FechaDeCreacion] datetime  NOT NULL,
    [FechaDeModicacion] datetime  NOT NULL,
    [Creador] nvarchar(max)  NOT NULL,
    [Notas] nvarchar(max)  NOT NULL,
    [Activo] bit  NOT NULL,
    [Direccione_Id] int  NOT NULL
);
GO

-- Creating table 'Direcciones'
CREATE TABLE [dbo].[Direcciones] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Alimentoes'
CREATE TABLE [dbo].[Alimentoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Precio] float  NOT NULL,
    [CostoDeIngredientes] float  NOT NULL,
    [FechaDeCreacion] datetime  NOT NULL,
    [FechaDeModificacion] datetime  NOT NULL,
    [Activo] bit  NOT NULL,
    [Codigo] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AlimentoPedidoes'
CREATE TABLE [dbo].[AlimentoPedidoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Cantidad] int  NOT NULL,
    [Pedido_Id] int  NOT NULL,
    [Alimento_Id] int  NOT NULL
);
GO

-- Creating table 'AlimentoIngredientes'
CREATE TABLE [dbo].[AlimentoIngredientes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Cantidad] int  NOT NULL,
    [Ingredientes_Id] int  NOT NULL,
    [Alimento_Id] int  NOT NULL
);
GO

-- Creating table 'Empleadoes'
CREATE TABLE [dbo].[Empleadoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Contraseña] nvarchar(max)  NOT NULL,
    [NombreDeUsuario] nvarchar(max)  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [FechaDeCreacion] datetime  NOT NULL,
    [FechaDeModicacion] datetime  NOT NULL,
    [Creador] nvarchar(max)  NOT NULL,
    [Activo] bit  NOT NULL,
    [TipoDeEmpleado] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Productoes'
CREATE TABLE [dbo].[Productoes] (
    [CodigoDeBarras] nvarchar(max)  NOT NULL,
    [Precio] float  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'ProductoPedidoes'
CREATE TABLE [dbo].[ProductoPedidoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Cantidad] int  NOT NULL,
    [Pedido_Id] int  NOT NULL,
    [Productoes_Id] int  NOT NULL
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
    [Empleado_Id] int  NOT NULL
);
GO

-- Creating table 'IngredienteIngredientes'
CREATE TABLE [dbo].[IngredienteIngredientes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Cantidad] float  NOT NULL,
    [IngredienteCompuesto_Id] int  NOT NULL,
    [IngredienteComponente_Id] int  NOT NULL
);
GO

-- Creating table 'Pedidoes_PedidoADomicilio'
CREATE TABLE [dbo].[Pedidoes_PedidoADomicilio] (
    [Notas] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL,
    [Clientes_Id] int  NOT NULL
);
GO

-- Creating table 'Pedidoes_PedidoLocal'
CREATE TABLE [dbo].[Pedidoes_PedidoLocal] (
    [Id] int  NOT NULL,
    [Cuenta_Id] int  NOT NULL
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

-- Creating primary key on [Id] in table 'Pedidoes'
ALTER TABLE [dbo].[Pedidoes]
ADD CONSTRAINT [PK_Pedidoes]
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

-- Creating primary key on [Id] in table 'Alimentoes'
ALTER TABLE [dbo].[Alimentoes]
ADD CONSTRAINT [PK_Alimentoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AlimentoPedidoes'
ALTER TABLE [dbo].[AlimentoPedidoes]
ADD CONSTRAINT [PK_AlimentoPedidoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AlimentoIngredientes'
ALTER TABLE [dbo].[AlimentoIngredientes]
ADD CONSTRAINT [PK_AlimentoIngredientes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Empleadoes'
ALTER TABLE [dbo].[Empleadoes]
ADD CONSTRAINT [PK_Empleadoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Productoes'
ALTER TABLE [dbo].[Productoes]
ADD CONSTRAINT [PK_Productoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductoPedidoes'
ALTER TABLE [dbo].[ProductoPedidoes]
ADD CONSTRAINT [PK_ProductoPedidoes]
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

-- Creating primary key on [Id] in table 'IngredienteIngredientes'
ALTER TABLE [dbo].[IngredienteIngredientes]
ADD CONSTRAINT [PK_IngredienteIngredientes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pedidoes_PedidoADomicilio'
ALTER TABLE [dbo].[Pedidoes_PedidoADomicilio]
ADD CONSTRAINT [PK_Pedidoes_PedidoADomicilio]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pedidoes_PedidoLocal'
ALTER TABLE [dbo].[Pedidoes_PedidoLocal]
ADD CONSTRAINT [PK_Pedidoes_PedidoLocal]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Ingredientes_Id] in table 'AlimentoIngredientes'
ALTER TABLE [dbo].[AlimentoIngredientes]
ADD CONSTRAINT [FK_AlimentoIngredienteIngrediente]
    FOREIGN KEY ([Ingredientes_Id])
    REFERENCES [dbo].[Ingredientes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlimentoIngredienteIngrediente'
CREATE INDEX [IX_FK_AlimentoIngredienteIngrediente]
ON [dbo].[AlimentoIngredientes]
    ([Ingredientes_Id]);
GO

-- Creating foreign key on [Empleado_Id] in table 'Pedidoes'
ALTER TABLE [dbo].[Pedidoes]
ADD CONSTRAINT [FK_EmpleadoPedido]
    FOREIGN KEY ([Empleado_Id])
    REFERENCES [dbo].[Empleadoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoPedido'
CREATE INDEX [IX_FK_EmpleadoPedido]
ON [dbo].[Pedidoes]
    ([Empleado_Id]);
GO

-- Creating foreign key on [Pedido_Id] in table 'AlimentoPedidoes'
ALTER TABLE [dbo].[AlimentoPedidoes]
ADD CONSTRAINT [FK_PedidoAlimentoPedido]
    FOREIGN KEY ([Pedido_Id])
    REFERENCES [dbo].[Pedidoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PedidoAlimentoPedido'
CREATE INDEX [IX_FK_PedidoAlimentoPedido]
ON [dbo].[AlimentoPedidoes]
    ([Pedido_Id]);
GO

-- Creating foreign key on [Clientes_Id] in table 'Pedidoes_PedidoADomicilio'
ALTER TABLE [dbo].[Pedidoes_PedidoADomicilio]
ADD CONSTRAINT [FK_PedidaADomicilioCliente]
    FOREIGN KEY ([Clientes_Id])
    REFERENCES [dbo].[Clientes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PedidaADomicilioCliente'
CREATE INDEX [IX_FK_PedidaADomicilioCliente]
ON [dbo].[Pedidoes_PedidoADomicilio]
    ([Clientes_Id]);
GO

-- Creating foreign key on [Direccione_Id] in table 'Clientes'
ALTER TABLE [dbo].[Clientes]
ADD CONSTRAINT [FK_DireccionesCliente]
    FOREIGN KEY ([Direccione_Id])
    REFERENCES [dbo].[Direcciones]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DireccionesCliente'
CREATE INDEX [IX_FK_DireccionesCliente]
ON [dbo].[Clientes]
    ([Direccione_Id]);
GO

-- Creating foreign key on [Alimento_Id] in table 'AlimentoPedidoes'
ALTER TABLE [dbo].[AlimentoPedidoes]
ADD CONSTRAINT [FK_AlimentoAlimentoPedido]
    FOREIGN KEY ([Alimento_Id])
    REFERENCES [dbo].[Alimentoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlimentoAlimentoPedido'
CREATE INDEX [IX_FK_AlimentoAlimentoPedido]
ON [dbo].[AlimentoPedidoes]
    ([Alimento_Id]);
GO

-- Creating foreign key on [Alimento_Id] in table 'AlimentoIngredientes'
ALTER TABLE [dbo].[AlimentoIngredientes]
ADD CONSTRAINT [FK_AlimentoAlimentoIngrediente]
    FOREIGN KEY ([Alimento_Id])
    REFERENCES [dbo].[Alimentoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AlimentoAlimentoIngrediente'
CREATE INDEX [IX_FK_AlimentoAlimentoIngrediente]
ON [dbo].[AlimentoIngredientes]
    ([Alimento_Id]);
GO

-- Creating foreign key on [Pedido_Id] in table 'ProductoPedidoes'
ALTER TABLE [dbo].[ProductoPedidoes]
ADD CONSTRAINT [FK_PedidoProductoPedido]
    FOREIGN KEY ([Pedido_Id])
    REFERENCES [dbo].[Pedidoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PedidoProductoPedido'
CREATE INDEX [IX_FK_PedidoProductoPedido]
ON [dbo].[ProductoPedidoes]
    ([Pedido_Id]);
GO

-- Creating foreign key on [Productoes_Id] in table 'ProductoPedidoes'
ALTER TABLE [dbo].[ProductoPedidoes]
ADD CONSTRAINT [FK_ProductoPedidoProducto]
    FOREIGN KEY ([Productoes_Id])
    REFERENCES [dbo].[Productoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductoPedidoProducto'
CREATE INDEX [IX_FK_ProductoPedidoProducto]
ON [dbo].[ProductoPedidoes]
    ([Productoes_Id]);
GO

-- Creating foreign key on [Producto_Id] in table 'Ingredientes'
ALTER TABLE [dbo].[Ingredientes]
ADD CONSTRAINT [FK_ProductoIngrediente]
    FOREIGN KEY ([Producto_Id])
    REFERENCES [dbo].[Productoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductoIngrediente'
CREATE INDEX [IX_FK_ProductoIngrediente]
ON [dbo].[Ingredientes]
    ([Producto_Id]);
GO

-- Creating foreign key on [Empleado_Id] in table 'Cuentas'
ALTER TABLE [dbo].[Cuentas]
ADD CONSTRAINT [FK_EmpleadoCuenta]
    FOREIGN KEY ([Empleado_Id])
    REFERENCES [dbo].[Empleadoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoCuenta'
CREATE INDEX [IX_FK_EmpleadoCuenta]
ON [dbo].[Cuentas]
    ([Empleado_Id]);
GO

-- Creating foreign key on [Cuentas_Id] in table 'Mesas'
ALTER TABLE [dbo].[Mesas]
ADD CONSTRAINT [FK_MesaCuenta]
    FOREIGN KEY ([Cuentas_Id])
    REFERENCES [dbo].[Cuentas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MesaCuenta'
CREATE INDEX [IX_FK_MesaCuenta]
ON [dbo].[Mesas]
    ([Cuentas_Id]);
GO

-- Creating foreign key on [Cuenta_Id] in table 'Pedidoes_PedidoLocal'
ALTER TABLE [dbo].[Pedidoes_PedidoLocal]
ADD CONSTRAINT [FK_CuentaPedidoLocal]
    FOREIGN KEY ([Cuenta_Id])
    REFERENCES [dbo].[Cuentas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CuentaPedidoLocal'
CREATE INDEX [IX_FK_CuentaPedidoLocal]
ON [dbo].[Pedidoes_PedidoLocal]
    ([Cuenta_Id]);
GO

-- Creating foreign key on [IngredienteCompuesto_Id] in table 'IngredienteIngredientes'
ALTER TABLE [dbo].[IngredienteIngredientes]
ADD CONSTRAINT [FK_IngredienteIngredienteIngrediente]
    FOREIGN KEY ([IngredienteCompuesto_Id])
    REFERENCES [dbo].[Ingredientes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IngredienteIngredienteIngrediente'
CREATE INDEX [IX_FK_IngredienteIngredienteIngrediente]
ON [dbo].[IngredienteIngredientes]
    ([IngredienteCompuesto_Id]);
GO

-- Creating foreign key on [IngredienteComponente_Id] in table 'IngredienteIngredientes'
ALTER TABLE [dbo].[IngredienteIngredientes]
ADD CONSTRAINT [FK_IngredienteIngredienteIngrediente1]
    FOREIGN KEY ([IngredienteComponente_Id])
    REFERENCES [dbo].[Ingredientes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IngredienteIngredienteIngrediente1'
CREATE INDEX [IX_FK_IngredienteIngredienteIngrediente1]
ON [dbo].[IngredienteIngredientes]
    ([IngredienteComponente_Id]);
GO

-- Creating foreign key on [Id] in table 'Pedidoes_PedidoADomicilio'
ALTER TABLE [dbo].[Pedidoes_PedidoADomicilio]
ADD CONSTRAINT [FK_PedidoADomicilio_inherits_Pedido]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Pedidoes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Pedidoes_PedidoLocal'
ALTER TABLE [dbo].[Pedidoes_PedidoLocal]
ADD CONSTRAINT [FK_PedidoLocal_inherits_Pedido]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Pedidoes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------