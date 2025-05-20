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
    [Email] nvarchar(450) NOT NULL,
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
    CONSTRAINT [PK_PainelPeca] PRIMARY KEY ([PainelId], [PecaId]),
    CONSTRAINT [FK_PainelPeca_Painel_PainelId] FOREIGN KEY ([PainelId]) REFERENCES [Painel] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PainelPeca_Peca_PecaId] FOREIGN KEY ([PecaId]) REFERENCES [Peca] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Manutencao_PainelId] ON [Manutencao] ([PainelId]);

CREATE INDEX [IX_PainelPeca_PecaId] ON [PainelPeca] ([PecaId]);

CREATE UNIQUE INDEX [IX_Usuario_Email] ON [Usuario] ([Email]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250423002328_AdicionarChaveCompostaPainelPeca', N'9.0.4');

CREATE TABLE [SolicitacaoPainel] (
    [Id] int NOT NULL IDENTITY,
    [UsuarioId] int NOT NULL,
    [NomePainel] nvarchar(max) NOT NULL,
    [Descricao] nvarchar(max) NOT NULL,
    [DataSolicitacao] datetime2 NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_SolicitacaoPainel] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SolicitacaoPainel_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_SolicitacaoPainel_UsuarioId] ON [SolicitacaoPainel] ([UsuarioId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250429002402_AddSolicitacaoPainel', N'9.0.4');

ALTER TABLE [Painel] ADD [Nome] nvarchar(max) NOT NULL DEFAULT N'';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250429012108_AddNomePainel', N'9.0.4');

ALTER TABLE [Painel] ADD [UsuarioId] int NOT NULL DEFAULT 0;

CREATE INDEX [IX_Painel_UsuarioId] ON [Painel] ([UsuarioId]);

ALTER TABLE [Painel] ADD CONSTRAINT [FK_Painel_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250429015626_AddUsuarioRelationInPainel', N'9.0.4');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250430150508_AjustePainelPeca', N'9.0.4');

ALTER TABLE [Painel] DROP CONSTRAINT [FK_Painel_Usuario_UsuarioId];

ALTER TABLE [Painel] ADD CONSTRAINT [FK_Painel_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250501031245_RemocaoRequiredUsuarioId', N'9.0.4');

CREATE TABLE [SolicitacaoManutencao] (
    [Id] int NOT NULL IDENTITY,
    [UsuarioId] int NOT NULL,
    [Descricao] nvarchar(max) NOT NULL,
    [DataSolicitacao] datetime2 NOT NULL,
    [StatusManutencao] int NOT NULL,
    CONSTRAINT [PK_SolicitacaoManutencao] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SolicitacaoManutencao_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_SolicitacaoManutencao_UsuarioId] ON [SolicitacaoManutencao] ([UsuarioId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250504235824_AddSolicitacaoManutencao', N'9.0.4');

ALTER TABLE [SolicitacaoManutencao] ADD [PainelId] int NOT NULL DEFAULT 0;

CREATE INDEX [IX_SolicitacaoManutencao_PainelId] ON [SolicitacaoManutencao] ([PainelId]);

ALTER TABLE [SolicitacaoManutencao] ADD CONSTRAINT [FK_SolicitacaoManutencao_Painel_PainelId] FOREIGN KEY ([PainelId]) REFERENCES [Painel] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250505162001_AddPainelIdTabelaSolicitacaoManutencao', N'9.0.4');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250505180221_VinculoPainelManutencao', N'9.0.4');

COMMIT;
GO


