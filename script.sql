Build started...
Build succeeded.
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Painel] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] nvarchar(max) NOT NULL,
    [Altura] float NOT NULL,
    [Largura] float NOT NULL,
    [Comprimento] float NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    [DataAlteracao] datetime2 NULL,
    CONSTRAINT [PK_Painel] PRIMARY KEY ([Id])
);

CREATE TABLE [Peca] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Descricao] nvarchar(max) NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    [DataAlteracao] datetime2 NULL,
    CONSTRAINT [PK_Peca] PRIMARY KEY ([Id])
);

CREATE TABLE [Usuario] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Senha] nvarchar(max) NOT NULL,
    [Telefone] nvarchar(20) NULL,
    [Tipo] int NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    [DataAlteracao] datetime2 NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([Id])
);

CREATE TABLE [Manutencao] (
    [Id] int NOT NULL IDENTITY,
    [dataManutencao] datetime2 NOT NULL,
    [Descricao] nvarchar(max) NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    [DataAlteracao] datetime2 NULL,
    [PainelId] int NOT NULL,
    CONSTRAINT [PK_Manutencao] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Manutencao_Painel_PainelId] FOREIGN KEY ([PainelId]) REFERENCES [Painel] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [PainelPeca] (
    [PainelId] int NOT NULL,
    [PecaId] int NOT NULL,
    [Quantidade] int NOT NULL,
    [DataInstalacao] datetime2 NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    [DataAtualizacao] datetime2 NULL,
    [Status] int NOT NULL,
    CONSTRAINT [FK_PainelPeca_Painel_PainelId] FOREIGN KEY ([PainelId]) REFERENCES [Painel] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PainelPeca_Peca_PecaId] FOREIGN KEY ([PecaId]) REFERENCES [Peca] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Manutencao_PainelId] ON [Manutencao] ([PainelId]);

CREATE INDEX [IX_PainelPeca_PainelId] ON [PainelPeca] ([PainelId]);

CREATE INDEX [IX_PainelPeca_PecaId] ON [PainelPeca] ([PecaId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250418015800_Tabelas', N'9.0.4');

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuario]') AND [c].[name] = N'Email');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Usuario] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [Usuario] ALTER COLUMN [Email] nvarchar(450) NOT NULL;

CREATE UNIQUE INDEX [IX_Usuario_Email] ON [Usuario] ([Email]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250421135744_AdicionarIndiceUnicoEmail', N'9.0.4');

COMMIT;
GO


