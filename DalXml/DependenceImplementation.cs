namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


internal class DependenceImplementation:IDependence
{
    readonly string s_dependence_xml = "dependences";

    public int Create(Dependence newDependence)
    {
        //Checking if the directory exists
        //If it doesn't exist then create it
        if (!Directory.Exists(s_dependence_xml))
            Directory.CreateDirectory(s_dependence_xml);

        //Create root for XML file
        XElement newRootXElement = new XElement("ArrayOfDependence");

        //Inserting values from an received entity into an XElement
        XElement newDependenceElement = new XElement("Dependence", new XElement("IdNumber", Config.nextNumberOfDependenceTask),
                                                    new XElement("TaskNumberDepends", newDependence.TaskNumberDepends),
                                                    new XElement("PreviousTaskDepends", newDependence.PreviousTaskDepends),
                                                    new XElement("Erasable", newDependence.erasable));
        //Adding the element to the XML file
        newDependenceElement.Add(newDependence);

        //Saving XElement to XML file
        newDependenceElement.Save(s_dependence_xml);
        
        return newDependence.TaskNumberDepends;
    }

    public void Delete(int id)
    {
        if (Read(id) is null)
            throw new DalDoesNotExistException($"Task depends with number = {id} does not exist");
        if (Read(id)!.erasable == true)
            throw new DalDeletionImpossibleException($"Task depends with number = {id} cannot be deleted");

        XElement reDependenceElement = XMLTools.LoadListFromXMLElement(s_dependence_xml);
        var deleteElement = reDependenceElement.Elements().FirstOrDefault(idDependence => (int?)idDependence.Element("IdNumber") == id);

        deleteElement!.Remove();
        XMLTools.SaveListToXMLElement(reDependenceElement, s_dependence_xml);
    }

    public Dependence? Read(int id)
    {
        //Loading from an XML file into an XElement
        XElement reDependenceElement = XMLTools.LoadListFromXMLElement(s_dependence_xml);

        //Search in XElement by ID number
        //If found will return an entity
        //If not found will return null
        var element = reDependenceElement.Elements().FirstOrDefault(idDependence => (int?)idDependence.Element("IdNumber") == id);

        if (element is null)
            return null;

        //Conversion of the fields in XElement to a task that will return
        int idTask = int.Parse(reDependenceElement.Element("IdNumber")!.Value); 
        int numberTask = int.Parse(reDependenceElement.Element("TaskNumberDepends")!.Value);
        int previousTask = int.Parse(reDependenceElement.Element("PreviousTaskDepends")!.Value);

        //Entry of all values into a new task
        Dependence? dependence = new(idTask, numberTask, previousTask);

        return dependence;
    }

    public Dependence? Read(Func<Dependence, bool> filter)
    {
        XElement reDependenceElement = XMLTools.LoadListFromXMLElement(s_dependence_xml);
        IEnumerable<XElement> allElements = reDependenceElement.Elements();
        List<Dependence?> listDependences = new();

        //We will go through all the elements until we find the requested element
        foreach (var element in allElements)
        {
            Dependence newNode = new Dependence(Convert.ToInt32(element.Element("IdNumber")!.Value),
                                                    int.Parse(element.Element("TaskNumberDepends")!.Value),
                                                    int.Parse(element.Element("PreviousTaskDepends")!.Value));
            if (filter(newNode))
                return newNode;
        }
        return null;
    }

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    {
        XElement reDependenceElement = XMLTools.LoadListFromXMLElement(s_dependence_xml);
        IEnumerable<XElement> allElements = reDependenceElement.Elements();
        List<Dependence?> listDependences = new();


        //If we don't have any condition it means that we have to return the whole element
        //Therefore we will create a new element each time and add to the list
        //And in the end we will return her
        if (filter == null)
        {
            foreach (var element in allElements)
            {
                Dependence newNode = new Dependence(Convert.ToInt32(element.Element("IdNumber")!.Value),
                                                    int.Parse(element.Element("TaskNumberDepends")!.Value),
                                                    int.Parse(element.Element("PreviousTaskDepends")!.Value));
                listDependences.Add(newNode);
            }
            return listDependences;
        }
        //If we have any condition, we will pass it to a column of a list and check on it if the condition is met
        //If so, we will put it on the list
        else
        {
            foreach (var element in allElements)
            {
                Dependence newNode = new Dependence(Convert.ToInt32(element.Element("IdNumber")!.Value),
                                                        int.Parse(element.Element("TaskNumberDepends")!.Value),
                                                        int.Parse(element.Element("PreviousTaskDepends")!.Value));
                if (filter(newNode))
                    listDependences.Add(newNode);
            }
            return listDependences;
        }
    }

    public void Update(Dependence newDependence)
    {
        if (Read(newDependence.DependencyIdNumber) is null)
            throw new DalDoesNotExistException($"Task depends with number = {newDependence.DependencyIdNumber} does not exist");

        XElement reDependenceElement = XMLTools.LoadListFromXMLElement(s_dependence_xml);
        var deleteElement = reDependenceElement.Elements().FirstOrDefault(idDependence => (int?)idDependence.Element("IdNumber") == newDependence.DependencyIdNumber);

        deleteElement!.Remove();
        reDependenceElement!.Add(newDependence);
        XMLTools.SaveListToXMLElement(reDependenceElement, s_dependence_xml);
    }
}