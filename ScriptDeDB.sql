

SET QUOTED_IDENTIFIER OFF;
GO
USE [SAIPDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

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
    [Codigo] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Mesas'
CREATE TABLE [dbo].[Mesas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Estado] nvarchar(max)  NOT NULL,
    [Cuenta_Id] int  NOT NULL
);
GO

-- Creating table 'Pedidos'
CREATE TABLE [dbo].[Pedidos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FechaDeCreacion] datetime  NOT NULL,
    [Creador] nvarchar(max)  NOT NULL,
    [PrecioTotal] float  NOT NULL,
    [Iva] float  NOT NULL,
    [Estado] nvarchar(max)  NOT NULL,
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
    [Creador] nvarchar(max)  NOT NULL,
    [Notas] nvarchar(max)  NOT NULL,
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
    [Codigo] nvarchar(max)  NOT NULL
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
    [Cantidad] int  NOT NULL,
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
    [Creador] nvarchar(max)  NOT NULL,
    [Activo] bit  NOT NULL,
    [TipoDeEmpleado] nvarchar(max)  NOT NULL,
    [Pedidos_Id] int  NOT NULL
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
    [Creador] nvarchar(max)  NOT NULL,
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
    [PrecioTotal] float  NOT NULL
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

-- Creating foreign key on [Pedidos_Id] in table 'Empleados'
ALTER TABLE [dbo].[Empleados]
ADD CONSTRAINT [FK_EmpleadoPedido]
    FOREIGN KEY ([Pedidos_Id])
    REFERENCES [dbo].[Pedidos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmpleadoPedido'
CREATE INDEX [IX_FK_EmpleadoPedido]
ON [dbo].[Empleados]
    ([Pedidos_Id]);
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

-- Creating foreign key on [Cuenta_Id] in table 'Mesas'
ALTER TABLE [dbo].[Mesas]
ADD CONSTRAINT [FK_MesaCuenta]
    FOREIGN KEY ([Cuenta_Id])
    REFERENCES [dbo].[Cuentas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MesaCuenta'
CREATE INDEX [IX_FK_MesaCuenta]
ON [dbo].[Mesas]
    ([Cuenta_Id]);
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