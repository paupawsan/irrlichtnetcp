#include "main.h"
extern "C"
{
	EXPORT IntPtr Event_Create() { return new SEvent(); }
    EXPORT EEVENT_TYPE Event_GetType(IntPtr event);
    EXPORT EMOUSE_INPUT_EVENT Event_GetMouseInputEvent(IntPtr event);
    EXPORT EGUI_EVENT_TYPE Event_GetGUIEventType(IntPtr event);
    EXPORT float Event_GetMouseWheelDelta(IntPtr event);
    EXPORT void Event_GetMousePosition(IntPtr event, M_POS2DS pos);
    EXPORT EKEY_CODE Event_GetKey(IntPtr event);
    EXPORT bool Event_GetKeyPressedDown(IntPtr event);
    EXPORT bool Event_GetKeyShift(IntPtr event);
    EXPORT bool Event_GetKeyControl(IntPtr event);
    EXPORT char Event_GetKeyChar(IntPtr event);
    EXPORT M_STRING Event_GetLogString(IntPtr event);
    EXPORT IntPtr Event_GetCaller(IntPtr event);
}
