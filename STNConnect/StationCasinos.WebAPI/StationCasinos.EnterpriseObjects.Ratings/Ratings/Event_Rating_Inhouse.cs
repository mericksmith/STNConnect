using StationCasinos.WebAPI.Utility;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

public partial class EventRatingInhouse : EventRating {
    
    //private EObjectRatingInhouse ratingField;
    
    [Required, ValidateObject]
    /// <remarks/>
    public EObjectRatingInhouse Rating { get; set;  }
}

public partial class EObjectRatingInhouse : EObjectRating {
    
    private EObjectRatingSessionInhouse sessionField;
    
    private EObjectRatingActionInhouse actionField;

    [Required, ValidateObject]
    public EObjectRatingSessionInhouse Session {
        get {
            return this.sessionField;
        }
        set {
            this.sessionField = value;
        }
    }

    [Required, ValidateObject]
    public EObjectRatingActionInhouse Action {
        get {
            return this.actionField;
        }
        set {
            this.actionField = value;
        }
    }
}

public partial class EObjectRatingSessionInhouse : EObjectRatingSession {
}

public partial class EObjectRatingSession {

    private string locationIdField;
    
    private string gameIdField;
    
    private System.DateTime gameDateField;
    
    private string shiftIdField;
    
    private string deptIdField;
    
    private string areaIdField;
    
    private System.DateTime startDatetimeField;

    
    /// <remarks/>
    [Required]
    [RegularExpression("^[0-9]*$", ErrorMessage = "locationId must be numeric")]
    public string locationId {
        get {
            return this.locationIdField;
        }
        set {
            this.locationIdField = value;
        }
    }

    /// <remarks/>
    [Required]
    [RegularExpression("^[0-9]*$", ErrorMessage = "gameId must be numeric")]
    public string gameId {
        get {
            return this.gameIdField;
        }
        set {
            this.gameIdField = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime gameDate {
        get {
            return this.gameDateField;
        }
        set {
            this.gameDateField = value;
        }
    }
    
    [StringLength(2)] //TODO, do any of the following three fields need to be longer than two?
    /// <remarks/>
    public string shiftId {
        get {
            return this.shiftIdField;
        }
        set {
            this.shiftIdField = value;
        }
    }

    [StringLength(2)]
    /// <remarks/>
    public string deptId {
        get {
            return this.deptIdField;
        }
        set {
            this.deptIdField = value;
        }
    }

    [StringLength(2)]
    /// <remarks/>
    public string areaId {
        get {
            return this.areaIdField;
        }
        set {
            this.areaIdField = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime startDatetime {
        get {
            return this.startDatetimeField;
        }
        set {
            this.startDatetimeField = value;
        }
    }
}

public partial class EObjectRatingAction {
    
    private ratingType ratingTypeField;
    
    private bool ratingFinalField;
    
    private System.DateTime endDateTimeField;
    
    private string timePlayedField;
    
    private string ratedByField;
    
    private string calculationIdField;
    
    private string calcTheoPctField;
    
    private string gamesField;
    
    private decimal aveBetField;
    
    private decimal turnOverField;
    
    private decimal actualWinField;
    
    private decimal theoWinField;
    
    private decimal markerBuyInField;
    
    private decimal cpvBuyInField;

    private decimal watInField;

    private decimal otherBuyInField;
    
    private decimal cashBuyInField;
    
    private decimal totalBuyInField;
    
    private decimal progressiveWonField;
    
    [Range(0, 1)]
    /// <remarks/>
    public ratingType ratingType {
        get {
            return this.ratingTypeField;
        }
        set {
            this.ratingTypeField = value;
        }
    }
    
    /// <remarks/>
    public bool ratingFinal {
        get {
            return this.ratingFinalField;
        }
        set {
            this.ratingFinalField = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime endDateTime {
        get {
            return this.endDateTimeField;
        }
        set {
            this.endDateTimeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="integer")]
    public string timePlayed {
        get {
            return this.timePlayedField;
        }
        set {
            this.timePlayedField = value;
        }
    }
    
    [Required]
    /// <remarks/>
    public string ratedBy {
        get {
            return this.ratedByField;
        }
        set {
            this.ratedByField = value;
        }
    }
    
    /// <remarks/>
    public string calculationId {
        get {
            return this.calculationIdField;
        }
        set {
            this.calculationIdField = value;
        }
    }
    
    [Range(0, 100)]
    public string calcTheoPct {
        get {
            return this.calcTheoPctField;
        }
        set {
            this.calcTheoPctField = value;
        }
    }
    
    public string games {
        get {
            return this.gamesField;
        }
        set {
            this.gamesField = value;
        }
    }
    
    /// <remarks/>
    public decimal aveBet {
        get {
            return this.aveBetField;
        }
        set {
            this.aveBetField = value;
        }
    }
    
    /// <remarks/>
    public decimal turnOver {
        get {
            return this.turnOverField;
        }
        set {
            this.turnOverField = value;
        }
    }
    
    /// <remarks/>
    [Required]
    [Range(0, 9999999.99)]
    public decimal actualWin {
        get {
            return this.actualWinField;
        }
        set {
            this.actualWinField = value;
        }
    }
    
    /// <remarks/>
    public decimal theoWin {
        get {
            return this.theoWinField;
        }
        set {
            this.theoWinField = value;
        }
    }

    [Range(0, 9999999.99)]
    /// <remarks/>
    public decimal markerBuyIn {
        get {
            return this.markerBuyInField;
        }
        set {
            this.markerBuyInField = value;
        }
    }

    [Range(0, 9999999.99)]
    /// <remarks/>
    public decimal cpvBuyIn {
        get {
            return this.cpvBuyInField;
        }
        set {
            this.cpvBuyInField = value;
        }
    }

    [Range(0, 9999999.99)]
    /// <remarks/>
    public decimal watIn
    {
        get
        {
            return this.watInField;
        }
        set
        {
            this.watInField = value;
        }
    }

    [Range(0, 9999999.99)]
    /// <remarks/>
    public decimal otherBuyIn {
        get {
            return this.otherBuyInField;
        }
        set {
            this.otherBuyInField = value;
        }
    }

    [Range(0, 9999999.99)]
    /// <remarks/>
    public decimal cashBuyIn {
        get {
            return this.cashBuyInField;
        }
        set {
            this.cashBuyInField = value;
        }
    }
      
    [Required]
    [Range(0, 9999999.99)]
    public decimal totalBuyIn {
        get {
            return this.totalBuyInField;
        }
        set {
            this.totalBuyInField = value;
        }
    }
    
    /// <remarks/>
    public decimal progressiveWon {
        get {
            return this.progressiveWonField;
        }
        set {
            this.progressiveWonField = value;
        }
    }
}

public enum ratingType {
    
    /// <remarks/>
    Interval,
    
    /// <remarks/>
    Update,
}

public partial class EObjectRatingActionInhouse : EObjectRatingAction {
    
    private decimal inhouseWonField;
    
    /// <remarks/>
    public decimal inhouseWon {
        get {
            return this.inhouseWonField;
        }
        set {
            this.inhouseWonField = value;
        }
    }
}

public partial class EObject {
    
    private string otherField;
    
    [StringLength(255)] //TODO Can this be 255? Does it matter?
    /// <remarks/>
    public string Other {
        get {
            return this.otherField;
        }
        set {
            this.otherField = value;
        }
    }
}

public partial class EObjectRating : EObject {
    
    private string ratingHostIdField;
    
    private string ratingSourceIdField;
    
    private ratingStatus ratingStatusField;
    
    private string patronIdField;
    
    private decimal pointsEarnedField;
    
    /// <remarks/>
    public string ratingHostId {
        get {
            return this.ratingHostIdField;
        }
        set {
            this.ratingHostIdField = value;
        }
    }
    
    [Required]
    public string ratingSourceId {
        get {
            return this.ratingSourceIdField;
        }
        set {
            this.ratingSourceIdField = value;
        }
    }

    /// <remarks/>
    [Required]
    public ratingStatus ratingStatus {
        get {
            return this.ratingStatusField;
        }
        set {
            this.ratingStatusField = value;
        }
    }
    
    [StringLength(7, MinimumLength = 7), Required]
    [RegularExpression("^[0-9]*$", ErrorMessage = "patronId must be numeric")]
    /// <remarks/>
    public string patronId {
        get {
            return this.patronIdField;
        }
        set {
            this.patronIdField = value;
        }
    }

    /// <remarks/>
    [Required]
    [Range(0, 9999999.99)]
    public decimal pointsEarned {
        get {
            return this.pointsEarnedField;
        }
        set {
            this.pointsEarnedField = value;
        }
    }
}

public enum ratingStatus {

    /// <remarks/>
    Unknown = 0,
    /// <remarks/>
    Open = 1,

    /// <remarks/>
    Close = 2,
    
    /// <remarks/>
    Update = 3,
    
    /// <remarks/>
    Void = 4,
    
    /// <remarks/>
    Delete = 5,
    
   
}

public partial class Source {
    
    private string classNameField;
    
    private string identityField;
    
    /// <remarks/>
    public string ClassName {
        get {
            return this.classNameField;
        }
        set {
            this.classNameField = value;
        }
    }
    
    /// <remarks/>
    public string Identity {
        get {
            return this.identityField;
        }
        set {
            this.identityField = value;
        }
    }
}

public partial class Event {
    
    private Source sourceField;
    
    private EventPriority priorityField;
    
    private bool priorityFieldSpecified;
    
    private string propertyCodeField;
    
    private System.DateTime timestampField;
    
    private string notesField;
    
    private bool isTestMessageField;
    
    private System.DateTime testMessageExpirationDateTimeField;
    
    /// <remarks/>
    public Source Source {
        get {
            return this.sourceField;
        }
        set {
            this.sourceField = value;
        }
    }
    
    /// <remarks/>
    public EventPriority Priority {
        get {
            return this.priorityField;
        }
        set {
            this.priorityField = value;
        }
    }
    
    public bool PrioritySpecified {
        get {
            return this.priorityFieldSpecified;
        }
        set {
            this.priorityFieldSpecified = value;
        }
    }


    /// <remarks/>
    [Required]
    [RegularExpression("^[0-9]*$", ErrorMessage = "PropertyCode must be numeric")]
    public string PropertyCode {
        get {
            return this.propertyCodeField;
        }
        set {
            this.propertyCodeField = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime Timestamp {
        get {
            return this.timestampField;
        }
        set {
            this.timestampField = value;
        }
    }

    [StringLength(255)] //TODO, what is the max this field can be for CMS/OASIS?
    /// <remarks/>
    public string Notes {
        get {
            return this.notesField;
        }
        set {
            this.notesField = value;
        }
    }
    
    /// <remarks/>
    public bool IsTestMessage {
        get {
            return this.isTestMessageField;
        }
        set {
            this.isTestMessageField = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime TestMessageExpirationDateTime {
        get {
            return this.testMessageExpirationDateTimeField;
        }
        set {
            this.testMessageExpirationDateTimeField = value;
        }
    }
}

public enum EventPriority {
    
    /// <remarks/>
    Highest,
    
    /// <remarks/>
    High,
    
    /// <remarks/>
    Normal,
    
    /// <remarks/>
    Low,
    
    /// <remarks/>
    Lowest,
}

public partial class EventRating : Event {
    
    private EventRatingTransAction transActionField;
    
    private System.DateTime transDateTimeField;
    
    private string transStationField;
    
    private string postedByField;
    
    private string approvedByField;
    
    /// <remarks/>
    public EventRatingTransAction transAction {
        get {
            return this.transActionField;
        }
        set {
            this.transActionField = value;
        }
    }
    
    [Required(AllowEmptyStrings = false)]
    /// <remarks/>
    public System.DateTime transDateTime {
        get {
            return this.transDateTimeField;
        }
        set {
            this.transDateTimeField = value;
        }
    }
    
    /// <remarks/>
    public string transStation {
        get {
            return this.transStationField;
        }
        set {
            this.transStationField = value;
        }
    }
    
    /// <remarks/>
    public string postedBy {
        get {
            return this.postedByField;
        }
        set {
            this.postedByField = value;
        }
    }
    
    /// <remarks/>
    public string approvedBy {
        get {
            return this.approvedByField;
        }
        set {
            this.approvedByField = value;
        }
    }
}

public enum EventRatingTransAction {
    
    /// <remarks/>
    Open,
    
    /// <remarks/>
    Close,
    
    /// <remarks/>
    Update,
    
    /// <remarks/>
    Void,
    
    /// <remarks/>
    Delete,
    
    /// <remarks/>
    Unknown,
}
