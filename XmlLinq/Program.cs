//  XmlLinq Console App

// 22 warnings and counting



using System;
using System.Data;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;


//  XML data  Elements and Attributes
XElement contacts =
new XElement ( "Contacts",
    new XElement ( "Contact",
        new XElement ( "Name", "Patrick Hines" ),
        new XElement ( "Phone", "206-555-0144",
            new XAttribute ( "Type", "Home" ) ),
        new XElement ( "phone", "425-555-0145",
            new XAttribute ( "Type", "Work" ) ),
        new XElement ( "Address",
            new XElement ( "Street1", "123 Main St" ),
            new XElement ( "City", "Mercer Island" ),
            new XElement ( "State", "WA" ),
            new XElement ( "Postal", "68042" )
        )
    )
);



static void LoadXmlFile ()
{
    var filename = "PurchaseOrder.xml";
    var currentDirectory = Directory.GetCurrentDirectory ();
    var purchaseOrderFilepath = Path.Combine (currentDirectory, filename);

    XElement purchaseOrder = XElement.Load (purchaseOrderFilepath);

    IEnumerable<string> partNos = from item in purchaseOrder.Descendants ( "Item" )
                                  select (string?)item.Attribute ( "PartNumber" );



    XElement purchaseOrder1 = XElement.Load ( purchaseOrderFilepath );
    IEnumerable<string> partNos1 = purchaseOrder1.Descendants ( "Item" ).Select ( x =>
        (string)x.Attribute ( "PartNumber" ) );
    

    IEnumerable<XElement> pricesByPartNos = from item in purchaseOrder.Descendants ( "Item" )
                                            where (int)item.Element ( "Quantity" ) * (decimal)item.Element ( "USPrice" ) > 100
                                            orderby (string)item.Element ( "PartNumber" )
                                            select item;

    IEnumerable<XElement> pricesByPartNos1 = purchaseOrder.Descendants ( "Item" )
                                        .Where ( item => (int)item.Element ( "Quantity" ) * (decimal)item.Element ( "USPrice" ) > 100 )
                                        .OrderBy ( order => order.Element ( "PartNumber" ) );
}


 void SerializeDataSet ( string filename )
{
    XmlSerializer ser = new XmlSerializer ( typeof ( DataSet ) );

    // Creates a DataSet; adds a table, column, and ten rows.
    DataSet ds = new DataSet ( "myDataSet" );
    DataTable t = new DataTable ( "table1" );
    DataColumn c = new DataColumn ( "thing" );
    t.Columns.Add ( c );
    ds.Tables.Add ( t );
    DataRow r;

    //  Add ten rows.
    for (int i = 0; i < 10; i++)
    {
        r = t.NewRow ();
        r [0] = "Thing " + i;
        t.Rows.Add ( r );
    }

    TextWriter writer = new StreamWriter ( filename );
    ser.Serialize ( writer, ds );
    writer.Close ();
}





#region  PO example
// The XmlRoot attribute allows you to set an alternate name
// (PurchaseOrder) for the XML element and its namespace. By
// default, the XmlSerializer uses the class name. The attribute
// also allows you to set the XML namespace for the element. Lastly,
// the attribute sets the IsNullable property, which specifies whether
// the xsi:null attribute appears if the class instance is set to
// a null reference.
[XmlRoot ( "PurchaseOrder", Namespace = "http://www.cpandl.com",
IsNullable = false )]
public class PurchaseOrder
{
    public Address ShipTo;
    public string OrderDate;
    // The XmlArray attribute changes the XML element name
    // from the default of "OrderedItems" to "Items".
    [XmlArray ( "Items" )]
    public OrderedItem [] OrderedItems;
    public decimal SubTotal;
    public decimal ShipCost;
    public decimal TotalCost;
}

public class Address
{
    // The XmlAttribute attribute instructs the XmlSerializer to serialize the
    // Name field as an XML attribute instead of an XML element (XML element is
    // the default behavior).
    [XmlAttribute]
    public string Name;
    public string Line1;

    // Setting the IsNullable property to false instructs the
    // XmlSerializer that the XML attribute will not appear if
    // the City field is set to a null reference.
    [XmlElement ( IsNullable = false )]
    public string City;
    public string State;
    public string Zip;
}

public class OrderedItem
{
    public string ItemName;
    public string Description;
    public decimal UnitPrice;
    public int Quantity;
    public decimal LineTotal;

    // Calculate is a custom method that calculates the price per item
    // and stores the value in a field.
    public void Calculate ()
    {
        LineTotal = UnitPrice * Quantity;
    }
}

public class Test
{
    public static void Main ()
    {
        // Read and write purchase orders.
        Test t = new Test ();
        t.CreatePO ( "po.xml" );
        t.ReadPO ( "po.xml" );
    }

    private void CreatePO ( string filename )
    {
        // Creates an instance of the XmlSerializer class;
        // specifies the type of object to serialize.
        XmlSerializer serializer = new XmlSerializer ( typeof ( PurchaseOrder ) );
        TextWriter writer = new StreamWriter ( filename );
        PurchaseOrder po = new PurchaseOrder ();

        // Creates an address to ship and bill to.
        Address billAddress = new Address ();
        billAddress.Name = "Teresa Atkinson";
        billAddress.Line1 = "1 Main St.";
        billAddress.City = "AnyTown";
        billAddress.State = "WA";
        billAddress.Zip = "00000";
        // Sets ShipTo and BillTo to the same addressee.
        po.ShipTo = billAddress;
        po.OrderDate = System.DateTime.Now.ToLongDateString ();

        // Creates an OrderedItem.
        OrderedItem i1 = new OrderedItem ();
        i1.ItemName = "Widget S";
        i1.Description = "Small widget";
        i1.UnitPrice = (decimal)5.23;
        i1.Quantity = 3;
        i1.Calculate ();

        // Inserts the item into the array.
        OrderedItem [] items = { i1 };
        po.OrderedItems = items;
        // Calculate the total cost.
        decimal subTotal = new decimal ();
        foreach (OrderedItem oi in items)
        {
            subTotal += oi.LineTotal;
        }
        po.SubTotal = subTotal;
        po.ShipCost = (decimal)12.51;
        po.TotalCost = po.SubTotal + po.ShipCost;
        // Serializes the purchase order, and closes the TextWriter.
        serializer.Serialize ( writer, po );
        writer.Close ();
    }

    protected void ReadPO ( string filename )
    {
        // Creates an instance of the XmlSerializer class;
        // specifies the type of object to be deserialized.
        XmlSerializer serializer = new XmlSerializer ( typeof ( PurchaseOrder ) );
        // If the XML document has been altered with unknown
        // nodes or attributes, handles them with the
        // UnknownNode and UnknownAttribute events.
        serializer.UnknownNode += new
        XmlNodeEventHandler ( serializer_UnknownNode );
        serializer.UnknownAttribute += new
        XmlAttributeEventHandler ( serializer_UnknownAttribute );

        // A FileStream is needed to read the XML document.
        FileStream fs = new FileStream ( filename, FileMode.Open );
        // Declares an object variable of the type to be deserialized.
        PurchaseOrder po;
        // Uses the Deserialize method to restore the object's state
        // with data from the XML document. */
        po = (PurchaseOrder)serializer.Deserialize ( fs );
        // Reads the order date.
        Console.WriteLine ( "OrderDate: " + po.OrderDate );

        // Reads the shipping address.
        Address shipTo = po.ShipTo;
        ReadAddress ( shipTo, "Ship To:" );
        // Reads the list of ordered items.
        OrderedItem [] items = po.OrderedItems;
        Console.WriteLine ( "Items to be shipped:" );
        foreach (OrderedItem oi in items)
        {
            Console.WriteLine ( "\t" +
            oi.ItemName + "\t" +
            oi.Description + "\t" +
            oi.UnitPrice + "\t" +
            oi.Quantity + "\t" +
            oi.LineTotal );
        }
        // Reads the subtotal, shipping cost, and total cost.
        Console.WriteLine (
        "\n\t\t\t\t\t Subtotal\t" + po.SubTotal +
        "\n\t\t\t\t\t Shipping\t" + po.ShipCost +
        "\n\t\t\t\t\t Total\t\t" + po.TotalCost
        );
    }

    protected void ReadAddress ( Address a, string label )
    {
        // Reads the fields of the Address.
        Console.WriteLine ( label );
        Console.Write ( "\t" +
        a.Name + "\n\t" +
        a.Line1 + "\n\t" +
        a.City + "\t" +
        a.State + "\n\t" +
        a.Zip + "\n" );
    }

    protected void serializer_UnknownNode
    ( object sender, XmlNodeEventArgs e )
    {
        Console.WriteLine ( "Unknown Node:" + e.Name + "\t" + e.Text );
    }

    protected void serializer_UnknownAttribute
    ( object sender, XmlAttributeEventArgs e )
    {
        System.Xml.XmlAttribute attr = e.Attr;
        Console.WriteLine ( "Unknown attribute " +
        attr.Name + "='" + attr.Value + "'" );
    }
}

#endregion

#region  Sample Data out
/*
<? xml version = "1.0" encoding = "utf-8" ?>
< PurchaseOrder xmlns:xsi = "htt p://www.w3.org/2001/XMLSchema-instance" xmlns: xsd = "htt p://www.w3.org/2001/XMLSchema" xmlns = "htt p://www.cpandl.com" >
    < ShipTo Name = "Teresa Atkinson" >
        < Line1 > 1 Main St.</ Line1 >
        < City > AnyTown </ City >
        < State > WA </ State >
        < Zip > 00000 </ Zip >
    </ ShipTo >
    < OrderDate > Wednesday, June 27, 2001</OrderDate>
    <Items>
        <OrderedItem>
            <ItemName>Widget S</ItemName>
            <Description>Small widget</Description>
            <UnitPrice>5.23</UnitPrice>
            <Quantity>3</Quantity>
            <LineTotal>15.69</LineTotal>
        </OrderedItem>
    </Items>
    <SubTotal>15.69</SubTotal>
    <ShipCost>12.51</ShipCost>
    <TotalCost>28.2</TotalCost>
</PurchaseOrder>
*/
    #endregion














/*
static void queryXml ()
{
    var filename = "PurchaseOrder.xml";
    
}
*/





