using System;
using System.Data;

public class BA_tblDealer
{
    DA_tblDealer objDA_tblDealer = new DA_tblDealer();

    public BA_tblDealer() { }

    private int _DealerID;
    public int DealerID
    {
        get { return _DealerID; }
        set { _DealerID = value; }
    }

    private string _DealerCode;
    public string DealerCode
    {
        get { return _DealerCode; }
        set { _DealerCode = value; }
    }

    private string _DealerName;
    public string DealerName
    {
        get { return _DealerName; }
        set { _DealerName = value; }
    }

    private string _Address;
    public string Address
    {
        get { return _Address; }
        set { _Address = value; }
    }

    private string _ContactName;
    public string ContactName
    {
        get { return _ContactName; }
        set { _ContactName = value; }
    }

    private string _Area;
    public string Area
    {
        get { return _Area; }
        set { _Area = value; }
    }

    private string _Pincode;
    public string Pincode
    {
        get { return _Pincode; }
        set { _Pincode = value; }
    }

    private string _Phone;
    public string Phone
    {
        get { return _Phone; }
        set { _Phone = value; }
    }

    private string _GST;
    public string GST
    {
        get { return _GST; }
        set { _GST = value; }
    }

    private Boolean _Isdeleted;
    public Boolean Isdeleted
    {
        get { return _Isdeleted; }
        set { _Isdeleted = value; }
    }

    private Int64 _CreateBy;
    public Int64 CreateBy
    {
        get { return _CreateBy; }
        set { _CreateBy = value; }
    }

    private string _CreateDate;
    public string CreateDate
    {
        get { return _CreateDate; }
        set { _CreateDate = value; }
    }

    private Int64 _UpdateBy;
    public Int64 UpdateBy
    {
        get { return _UpdateBy; }
        set { _UpdateBy = value; }
    }

    private string _UpdateDate;
    public string UpdateDate
    {
        get { return _UpdateDate; }
        set { _UpdateDate = value; }
    }

    private int _IsDealerCode;
    public int IsDealerCode
    {
        get { return _IsDealerCode; }
        set { _IsDealerCode = value; }
    }

    private long _StateID;
    public long StateID { get { return _StateID; } set { _StateID = value; } }

    private string _GSTPhoto;
    public string GSTPhoto { get { return _GSTPhoto; } set { _GSTPhoto = value; } }

    private string _VisitCard;
    public string VisitCard { get { return _VisitCard; } set { _VisitCard = value; } }

    public bool INSERT_tblDealer()
    {
        try
        {
            return objDA_tblDealer.INSERT_tblDealer(this);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public int INSERT_tblDealer_api()
    {
        try
        {
            return objDA_tblDealer.INSERT_tblDealer_api(this);
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public bool SELECT_ALL_tblDealer(ref DataTable dt)
    {
        try
        {
            if (objDA_tblDealer.SELECT_ALL_tblDealer(this, ref dt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DELETE_RECORDS_FROM_tblDealer()
    {
        try
        {
            if (objDA_tblDealer.DELETE_RECORDS_FROM_tblDealer(this))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblDealer(ref DataTable dt)
    {
        try
        {
            if (objDA_tblDealer.GET_RECORDS_FROM_tblDealer(this, ref dt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblDealer_ByCode(ref DataTable dt)
    {
        try
        {
            if (objDA_tblDealer.GET_RECORDS_FROM_tblDealer_ByCode(this, ref dt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool UPDATE_tblDealer()
    {
        try
        {
            if (objDA_tblDealer.UPDATE_tblDealer(this))
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    //  *****************************************************************************
    //  You Can use following code to fillup form
    //  *****************************************************************************
    //  objBA_tblDealer.DealerID = "";
    //  objBA_tblDealer.DealerCode = "";
    //  objBA_tblDealer.DealerName = "";
    //  objBA_tblDealer.Address = "";
    //  objBA_tblDealer.Area = "";
    //  objBA_tblDealer.Phone = "";
    //  objBA_tblDealer.GST = "";
    //  objBA_tblDealer.Transport = "";
    //  objBA_tblDealer.Isdeleted = "";
    //  objBA_tblDealer.CreateBy = "";
    //  objBA_tblDealer.CreateDate = "";
    //  objBA_tblDealer.UpdateBy = "";
    //  objBA_tblDealer.UpdateDate = "";
}