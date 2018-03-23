USE LrmsDb

CREATE TABLE [dbo].[ReadModel-Payment](
	[Json] [nvarchar](max),

	-- -------------------------------------------------
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AggregateId] [nvarchar](64) NOT NULL,

	CONSTRAINT [PK_ReadModel-Payment] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)

CREATE UNIQUE NONCLUSTERED INDEX [IX_ReadModel-PaymentAggregate_AggregateId] ON [dbo].[ReadModel-Payment]
(
	[AggregateId] ASC
)