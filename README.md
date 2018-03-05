# sytelinedevtools
Developer tools for SyteLine development

EXTGEN Utilities are using the following Guidelines for creating EXTGENS.

EXTGEN Guidelines
Generally, EXTGEN Sp's are used when the internal functionality of an SP is being changed. For example, changing a internal calculation within the Sp or changing a data operation (i.e. adding UETs to a copy operation).  If the input parameters or the output data schema from the SP is changing then DO NOT use an EXTGEN.  Most of the time when editing an SP for a report an EXTGEN WILL NOT be used.  Standard report changes should be done with a new prefixed version of the original SP.

In an attempt to not copy code from the original SP and promote more upgrade-able code, it is better to do either post or pre-processing after the initial SP is run. This will allow you to call the existing code and just add your particular changes. This isn't always possible but should be used by default. If it isn't possible in your case then it must be documented as a risk for higher risk for upgrade.

Post Processing Sample - This updates a custom table after a transfer order DC transaction is processed

SET ANSI_NULLS ON

GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[EXTGEN_TransferOrderShipSp](
  @TrnNum               TrnNumType
, @TransferFromSite     SiteType
, @TransferToSite       SiteType
, @TransferFobSite      SiteType
, @TransferFromWhse     WhseType
, @TransferToWhse       WhseType
, @TrnLine              TrnLineType
, @TQtyShipped          QtyUnitType
, @TUM                  UMType
, @TShipDate            DateType
, @TFromLoc             LocType
, @TFromLot             LotType
, @TToLot               LotType
, @Title                LongListType
, @RemoteSiteLotProcess ListExistingCreateBothType
, @UseExistingSerials   ListYesNoType
, @SerialPrefix         SerialPrefixType
, @PReasonCode          ReasonCodeType
, @Infobar              InfobarType         OUTPUT
, @TImportDocId         ImportDocIdType
, @ExportDocId          ExportDocIdType
, @MoveZeroCostItem     ListYesNoType = NULL
, @EmpNum               EmpNumType    = NULL
) AS

BEGIN
DECLARE @Severity INT

IF @Infobar = 'InProcess'
BEGIN
SET @Infobar = ''
Return 1
END

Set @Infobar = 'InProcess'

    EXEC @Severity = TransferOrderShipSp
         @TrnNum = @TrnNum
         , @TransferFromSite = @TransferFromSite
         , @TransferToSite = @TransferToSite
         , @TransferFobSite = @TransferFobSite
         , @TransferFromWhse = @TransferFromWhse
         , @TransferToWhse = @TransferToWhse
         , @TrnLine = @TrnLine
         , @TQtyShipped = @TQtyShipped
         , @TUM = @TUM
         , @TShipDate = @TShipDate
         , @TFromLoc = @TFromLoc
         , @TFromLot = @TFromLot
         , @TToLot = @TToLot
         , @Title = @Title
         , @RemoteSiteLotProcess = @RemoteSiteLotProcess
         , @UseExistingSerials = @UseExistingSerials
         , @SerialPrefix = @SerialPrefix
         , @PReasonCode = @PReasonCode
         , @Infobar = @Infobar OUTPUT
         , @TImportDocId = @TImportDocId
         , @ExportDocId = @ExportDocId
         , @MoveZeroCostItem = @MoveZeroCostItem
         , @EmpNum = @EmpNum

IF @Severity = 0
BEGIN
UPDATE tss
SET tss.ship_flag = (CASE WHEN @TQtyShipped > 0 THEN 1 ELSE 0 END)
from dbo.[_SOLX_SHIP_Skid] tss INNER join _TECH_skid ts ON tss.skid = ts.skid
WHERE tss.co_num = @TrnNum AND tss.lot = @TFromLot
AND ts.ref_type = 'T'
IF @Infobar = 'InProcess'
SET @Infobar = ''
END
END
   -- End of Generic External Touch Point code.

RETURN @Severity
 
 
Pre-Process Sample - Syteline had a job copy routine that when used from the job copy screen has option to copy UETs, however when used in BOM creation from Job screen it didn't allow you to choose, so this always turns it on.
 
CREATE PROCEDURE [dbo].[EXTGEN_JobCopy1Sp] (
  @FromType NVARCHAR(8)
, @FromJob JobType
, @FromSuffix SuffixType
, @FromOperNumStart OperNumType
, @FromOperNumEnd OperNumType
, @FromOpt NVARCHAR(8) -- (L)abor, (M)aterial, (B)oth
, @ToType NVARCHAR(8)
, @ToItem ItemType
, @ToJob JobType output
, @ToSuffix SuffixType output
, @ToStat JobStatusType = null
, @EffectDate DateType
, @ToOpt NVARCHAR(8)  -- (I)nsert, (D)elete
, @InsertOperNum OperNumType
, @SetQtyAllocJob ListYesNoType = 1
, @OverwriteExistingJobroute ListYesNoType = 1
, @ToJobCoProductMix ListYesNoType
, @FromJobCoProductMix ListYesNoType
, @FromJobQtyReleased QtyUnitType
, @PSessionID  RowPointerType = Null
, @Infobar InfobarType output
, @FromMRP ListYesNoType = 0
, @PLN_Ref_Type MrpOrderTypeType = null
, @PLN_Ref_Num MrpOrderType = null
, @PLN_ref_suf MrpOrderLineType = null
, @CopyUetValues ListYesNoType = 0
) AS
DECLARE @Severity INT

IF @Infobar = 'InProcess'
Return 1

Set @Infobar = 'InProcess'

SET @CopyUetValues =  (CASE WHEN @FromType = 'C' THEN 1 ELSE @CopyUetValues END)  

EXEC @Severity = JobCopy1Sp
@FromType = @FromType
, @FromJob = @FromJob
, @FromSuffix = @FromSuffix
, @FromOperNumStart = @FromOperNumStart
, @FromOperNumEnd = @FromOperNumEnd
, @FromOpt = @FromOpt
, @ToType = @ToType
, @ToItem = @ToItem
, @ToJob = @ToJob output
, @ToSuffix = @ToSuffix output
, @ToStat = @ToStat
, @EffectDate = @EffectDate
, @ToOpt = @ToOpt
, @InsertOperNum = @InsertOperNum
, @SetQtyAllocJob = @SetQtyAllocJob
, @OverwriteExistingJobroute = @OverwriteExistingJobroute
, @ToJobCoProductMix = @ToJobCoProductMix
, @FromJobCoProductMix = @FromJobCoProductMix
, @FromJobQtyReleased = @FromJobQtyReleased
, @PSessionID = @PSessionID
, @Infobar = @Infobar output
, @FromMRP = @FromMRP
, @PLN_Ref_Type = @PLN_Ref_Type
, @PLN_Ref_Num = @PLN_Ref_Num
, @PLN_ref_suf = @PLN_ref_suf
, @CopyUetValues = @CopyUetValues


RETURN @Severity 
GO
