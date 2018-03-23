USE LrmsDb

CREATE TABLE [dbo].[ReadModel-Customer](
	[Json] [nvarchar](max),

	-- -------------------------------------------------
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AggregateId] [nvarchar](64) NOT NULL,

	CONSTRAINT [PK_ReadModel-Customer] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)

CREATE UNIQUE NONCLUSTERED INDEX [IX_ReadModel-CustomerAggregate_AggregateId] ON [dbo].[ReadModel-Customer]
(
	[AggregateId] ASC
)