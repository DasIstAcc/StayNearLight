using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class AttributeModifier
{
    //private static Logger LOGGER = LogUtils.getLogger();
    private double amount;
    private Operation operation;
    private Func<string> nameGetter;
    private int id;

    public AttributeModifier(string name, double amount, Operation operation) :
        this(new System.Random().Next(1, 1000), () => name, amount, operation) { }

    public AttributeModifier(int Id, string name, double amount, Operation operation) :
        this(Id, () => name, amount, operation) { }


    public AttributeModifier(int ID, Func<string> nameGetter, double amount, Operation operation)
    {
        id = ID;
        this.nameGetter = nameGetter;
        this.amount = amount;
        this.operation = operation;
    }

    public int GetId()
    {
        return id;
    }

    public string getName()
    {
        return nameGetter();
    }

    public Operation getOperation()
    {
        return operation;
    }

    public double getAmount()
    {
        return amount;
    }

    public override bool Equals(object other)
    {
        if (this == other)
        {
            return true;
        }
        else if (other != null && GetType() == other.GetType())
        {
            AttributeModifier attributemodifier = (AttributeModifier)other;
            return Equals(id, attributemodifier.id);
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }

    public string toString()
    {
        return "AttributeModifier{amount=" + amount + ", operation=" + operation + ", name='" + nameGetter() + "', id=" + id + "}";
    }

    //public CompoundTag save()
    //{
    //    CompoundTag compoundtag = new CompoundTag();
    //    compoundtag.putString("Name", getName());
    //    compoundtag.putDouble("Amount", amount);
    //    compoundtag.putInt("Operation", operation.toValue());
    //    compoundtag.putUUID("UUID", id);
    //    return compoundtag;
    //}

    
    //public static AttributeModifier load(CompoundTag tag)
    //{
    //    try
    //    {
    //        UUID uuid = tag.getUUID("UUID");
    //        Operation operation = Operation.fromValue(tag.getInt("Operation"));
    //        return new AttributeModifier(uuid, tag.getString("Name"), tag.getDouble("Amount"), operation);
    //    }
    //    catch (Exception exception)
    //    {
    //        //LOGGER.warn("Unable to create attribute: {}", (object)exception.GetMessage());
    //        return null;
    //    }
    //}

    public class Operation
    {
        public enum OperationType
        {
            ADDITION,
            MULTIPLY_BASE,
            MULTIPLY_TOTAL
        }

        public static Operation ADDITION = new Operation(OperationType.ADDITION);
        public static Operation MULTIPLY_BASE = new Operation(OperationType.MULTIPLY_BASE);
        public static Operation MULTIPLY_TOTAL = new Operation(OperationType.MULTIPLY_TOTAL);


        public OperationType type;

        private static Operation[] OPERATIONS = new Operation[] { new Operation(OperationType.ADDITION), new Operation(OperationType.MULTIPLY_BASE), new Operation(OperationType.MULTIPLY_TOTAL) };
        private int value;

        private Operation(int val)
        {
            value = val;
        }

        private Operation(OperationType type)
        {
            this.type = type;
        }

        public int toValue()
        {
            return value;
        }

        public static Operation fromValue(int val)
        {
            if (val >= 0 && val < OPERATIONS.Length)
            {
                return OPERATIONS[val];
            }
            else
            {
                throw new ArgumentException("No operation with value " + val);
            }
        }
    }
}