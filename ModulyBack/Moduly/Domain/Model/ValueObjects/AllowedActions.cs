namespace ModulyBack.Moduly.Domain.Model.ValueObjects
{
    public enum AllowedActionEnum
    {
        CREATE_MODULE,
        EDIT_MODULE,
        DELETE_MODULE,
        ASSIGN_PERK,
        EDIT_PERK,
        DELETE_PERK,
        CREATE_INVOICE,
        DELETE_INVOICE,
        EDIT_INVOICE,
        CREATE_PAYMENT,
        DELETE_PAYMENT,
        EDIT_PAYMENT,
        CREATE_BEING,
        DELETE_BEING,
        EDIT_BEING,
        MOVE_BEING,
        CLONE_BEING,
        VIEW_MODULE,
        VIEW_INVOICE,
        VIEW_PAYMENT,
        VIEW_BEING,
        CREATE_INVENTORY, // Added
        EDIT_INVENTORY,   // Added
        DELETE_INVENTORY, // Added
        VIEW_INVENTORY,   // Added
        NONE,
        ADMIN,
        EDIT_ESPECIFIC_MODULE
    }
}