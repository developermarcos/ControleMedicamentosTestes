﻿CREATE TABLE [dbo].[TbRequisicao]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Data] DATE NOT NULL, 
    [QuantidadeMedicamento] SMALLINT NOT NULL, 
    [Funcionario_Id] UNIQUEIDENTIFIER NOT NULL, 
    [Paciente_Id] UNIQUEIDENTIFIER NOT NULL, 
    [Medicamento_Id] UNIQUEIDENTIFIER NOT NULL
)
