USE LrmsDb

CREATE TABLE [dbo].[ReadModel-Invoice](
	[Json] [nvarchar](max),

	-- -------------------------------------------------
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AggregateId] [nvarchar](64) NOT NULL,

	CONSTRAINT [PK_ReadModel-Invoice] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)

CREATE UNIQUE NONCLUSTERED INDEX [IX_ReadModel-InvoiceAggregate_AggregateId] ON [dbo].[ReadModel-Invoice]
(
	[AggregateId] ASC
)