// See https://aka.ms/new-console-template for more information
internal class Program
{
    static void Main(string[] args)
    {
        //creates new hashtable of item length 11 
            //as it is prime hopefully reducing collisions
        HashTable hashTable = new HashTable(11);
        //adding all data
            //("{name}", {id, or etc})
        hashTable.Add("Mia", 123);
        hashTable.Add("Tim", 456);
        hashTable.Add("Bea", 645);
        hashTable.Add("Zoe", 089);
        hashTable.Add("Sue", 189);
        hashTable.Add("Len", 289);
        hashTable.Add("Moe", 489);
        hashTable.Add("Lou", 389);
        hashTable.Add("Rae", 589);
        hashTable.Add("Max", 689);
        hashTable.Add("Tod", 889);

        Console.WriteLine("Enter a name to find, enter to quit");
        string name = Console.ReadLine();
        while (name != "")
        {
            if (hashTable.Find(name) == -1) //if data is outside table range or not in table output:
            { Console.WriteLine($"{name} is not in the table\n"); }
            //if data is inside table range output:
            else { Console.WriteLine($"{name} is located in index {hashTable.Find(name)}"); }
            name = Console.ReadLine();
        }
    }
}
    
class HashTable
{
    private int size;
    private (string, object)[] table; //a tuple
    //https://www.tutorialspoint.com/how-to-easily-initialise-a-list-of-tuples-in-chash
    public HashTable(int size)
    {
        this.size = size; 
        table = new(string, object)[size];
    }
    private int Hash(string key) //Find value of string as ASCII
    {
        int HashIntValue = 0;

        //convert string to char array
        string FindKey = key;
        char[] charArray = FindKey.ToCharArray();

        //iterates through char array to add each int value for chars
        for (int i = 0; i <= charArray.Length - 1; i++ )
        { 
            HashIntValue += Convert.ToInt32(charArray[i]);
        }
        //returns total MOD of table 
        HashIntValue = HashIntValue%size;
        return HashIntValue;
    }
    public void Add(string key, object value)
    {
        string FindKey = key;
        int HashIndex = Hash(FindKey);
        Console.WriteLine($"\n-Initial- Hash calc as {HashIndex}");

        while (table[HashIndex].Item1 != null)
        {
            //adds 1 to index until space is found
            HashIndex = (HashIndex+1)%size;
            Console.WriteLine($"Hash calc updated to {HashIndex}");
        }
        table[HashIndex] = (key, value);
        Console.WriteLine($"{key} stored to location {HashIndex}");
    }
    public int Find(string key)
    {
        string FindKey = key;
        int HashIndex = Hash(FindKey);
        int HashIndexInitial = HashIndex; 
        Console.WriteLine($"\n-Initial- Hash calc as {HashIndex}");

        while (table[HashIndex].Item1 != FindKey)
        {
            //if space empty return -1 as it wont be in list
                //due to how linear probing works
            if(table[HashIndex].Item1 == "") 
            {
                Console.WriteLine($"{key} will not be in table");
                HashIndex = -1;
                break;
            }
            //adds 1 to index until item is found
            HashIndex = (HashIndex + 1) % size;
            Console.WriteLine($"Hash calc updated to {HashIndex}");
            //if table is full there will need to be catch to escape if not in list
                //checking if the intital test value has been checked again allows for this
            if (HashIndexInitial == HashIndex)
            {
                Console.WriteLine($"{key} will not be in table");
                HashIndex = -1;
                break;
            }
        }
        Console.WriteLine($"{key} found in location {HashIndex}");
        return HashIndex;
    }
}