CREATE TABLE [dbo].[TbFornecedor]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Nome] NVARCHAR(50) NOT NULL, 
    [Telefone] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(50) NULL, 
    [Cidade] NVARCHAR(50) NULL, 
    [Estado] NVARCHAR(50) NULL
)
