#include "main.h"

extern "C"
{
	EXPORT IntPtr Vertices_CreateVertex2TCoords() { return new S3DVertex2TCoords(); }
	EXPORT IntPtr Vertices_CreateVertex() { return new S3DVertex(); }

	EXPORT void Vertices_GetColor(IntPtr vertex, M_SCOLOR color, bool vertor2t);
	EXPORT void Vertices_GetNormal(IntPtr vertex, M_VECT3DF normal, bool vertor2t);
	EXPORT void Vertices_GetPos(IntPtr vertex, M_VECT3DF pos, bool vertor2t);
	EXPORT void Vertices_GetTCoords(IntPtr vertex, M_POS2DF tcoords, bool vertor2t);
	EXPORT void Vertices_GetTCoords2(IntPtr vertex, M_POS2DF tcoords);
	EXPORT void Vertices_SetColor(IntPtr vertex, M_SCOLOR color, bool vertor2t);
	EXPORT void Vertices_SetNormal(IntPtr vertex, M_VECT3DF normal, bool vertor2t);
	EXPORT void Vertices_SetPos(IntPtr vertex, M_VECT3DF pos, bool vertor2t);
	EXPORT void Vertices_SetTCoords(IntPtr vertex, M_POS2DF tcoords, bool vertor2t);
	EXPORT void Vertices_SetTCoords2(IntPtr vertex, M_POS2DF tcoords);
}
