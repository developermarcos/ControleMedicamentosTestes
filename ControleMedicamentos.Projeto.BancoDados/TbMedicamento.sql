CREATE TABLE [dbo].[TbMedicamento]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Nome] NVARCHAR(50) NOT NULL, 
    [Descricao] NVARCHAR(50) NOT NULL, 
    [Lote] NVARCHAR(50) NOT NULL, 
    [Validade] DATE NOT NULL,
    [QuantidadeDisponivel] NVARCHAR(50) NOT NULL, 
    [Fornecedor_Id] UNIQUEIDENTIFIER NOT NULL, 
)
