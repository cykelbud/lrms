USE LrmsDb

CREATE TABLE [dbo].[ReadModel-Payout](
	[Json] [nvarchar](max),

	-- -------------------------------------------------
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AggregateId] [nvarchar](64) NOT NULL,

	CONSTRAINT [PK_ReadModel-Payout] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)

CREATE UNIQUE NONCLUSTERED INDEX [IX_ReadModel-PayoutAggregate_AggregateId] ON [dbo].[ReadModel-Payout]
(
	[AggregateId] ASC
)