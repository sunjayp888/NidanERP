USE [LocalNidanERP]
GO

/****** Object:  View [dbo].[BatchPrePlacementSearchField]    Script Date: 19/03/2018 05:43:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[BatchPrePlacementSearchField]
AS 
SELECT 
		[BatchPrePlacementId]
      ,BP.[Name]
      ,BP.[BatchId]
	  ,BatchName=B.Name
      ,[ScheduledStartDate]
      ,[ScheduledEndDate]
      ,[CreatedBy]
	  ,CreatedByName=P.Title+' '+P.Forenames+' '+P.Surname
      ,BP.[CreatedDate]
      ,BP.[CentreId]
	  ,CentreName=C.Name
      ,BP.[OrganisationId],
	  BP.Remark,
		
		ISNULL(BP.[Name], '')+ISNULL(B.Name, '')+ISNULL(P.Forenames, '')+ISNULL(P.Surname, '')
		+ISNULL(C.Name, '')
		+ ISNULL(CONVERT(varchar,[ScheduledStartDate], 101), '') 
	  + ISNULL(CONVERT(varchar,[ScheduledStartDate], 103), '') 
	  + ISNULL(CONVERT(varchar,[ScheduledStartDate], 105), '') 
	  + ISNULL(CONVERT(varchar,[ScheduledStartDate], 126), '')
	  AS SearchField
FROM 
	BatchPrePlacement BP  WITH (NOLOCK) left join Batch B WITH (NOLOCK)
	on BP.BatchId = B.BatchId join Personnel P WITH(NOLOCk)
	on BP.CreatedBy=P.PersonnelId join Centre C WITH(NOLOCk)
	on BP.CentreId=C.CentreId



















GO


