USE LrmsDb

CREATE TABLE [dbo].[ReadModel-Assignment](
	[Json] [nvarchar](max),

	-- -------------------------------------------------
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AggregateId] [nvarchar](64) NOT NULL,

	CONSTRAINT [PK_ReadModel-Assignment] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)

CREATE UNIQUE NONCLUSTERED INDEX [IX_ReadModel-AssignmentAggregate_AggregateId] ON [dbo].[ReadModel-Assignment]
(
	[AggregateId] ASC
)