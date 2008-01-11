#include "vertices.h"


void Vertices_GetColor(IntPtr vertex, M_SCOLOR color, bool vertor2t)
{
	if(vertor2t)
	{
		S3DVertex vert = *((S3DVertex*)vertex);
		UM_SCOLOR(vert.Color, color);
	}
	else
	{
		S3DVertex2TCoords vert = *((S3DVertex2TCoords*)vertex);
		UM_SCOLOR(vert.Color, color);
	}
}

void Vertices_GetNormal(IntPtr vertex, M_VECT3DF normal, bool vertor2t)
{
	if(vertor2t)
	{
		S3DVertex vert = *((S3DVertex*)vertex);
		UM_VECT3DF(vert.Normal, normal);
	}
	else
	{
		S3DVertex2TCoords vert = *((S3DVertex2TCoords*)vertex);
		UM_VECT3DF(vert.Normal, normal);
	}
}

void Vertices_GetPos(IntPtr vertex, M_VECT3DF pos, bool vertor2t)
{
	if(vertor2t)
	{
		S3DVertex vert = *((S3DVertex*)vertex);
		UM_VECT3DF(vert.Pos, pos);
	}
	else
	{
		S3DVertex2TCoords vert = *((S3DVertex2TCoords*)vertex);
		UM_VECT3DF(vert.Pos, pos);
	}
}

void Vertices_GetTCoords(IntPtr vertex, M_POS2DF tcoords, bool vertor2t)
{
	if(vertor2t)
	{
		S3DVertex vert = *((S3DVertex*)vertex);
		UM_POS2DF(position2d<float>(vert.TCoords.X, vert.TCoords.Y), tcoords);
	}
	else
	{
		S3DVertex2TCoords vert = *((S3DVertex2TCoords*)vertex);
		UM_POS2DF(position2d<float>(vert.TCoords.X, vert.TCoords.Y), tcoords);
	}
}

void Vertices_GetTCoords2(IntPtr vertex, M_POS2DF tcoords)
{
	S3DVertex2TCoords vert = *((S3DVertex2TCoords*)vertex);	
	UM_POS2DF(position2d<float>(vert.TCoords2.X, vert.TCoords2.Y), tcoords);
}



void Vertices_SetColor(IntPtr vertex, M_SCOLOR color, bool vertor2t)
{
	if(vertor2t)
	{
		S3DVertex *vert = ((S3DVertex*)vertex);
		vert->Color = MU_SCOLOR(color);
	}
	else
	{
		S3DVertex2TCoords *vert = ((S3DVertex2TCoords*)vertex);
		vert->Color = MU_SCOLOR(color);
	}
}

void Vertices_SetNormal(IntPtr vertex, M_VECT3DF normal, bool vertor2t)
{
	if(vertor2t)
	{
		S3DVertex *vert = ((S3DVertex*)vertex);
		vert->Normal = MU_VECT3DF(normal);
	}
	else
	{
		S3DVertex2TCoords *vert = ((S3DVertex2TCoords*)vertex);
		vert->Normal = MU_VECT3DF(normal);
	}
}

void Vertices_SetPos(IntPtr vertex, M_VECT3DF pos, bool vertor2t)
{
	if(vertor2t)
	{
		S3DVertex *vert = ((S3DVertex*)vertex);
		vert->Pos = MU_VECT3DF(pos);
	}
	else
	{
		S3DVertex2TCoords *vert = ((S3DVertex2TCoords*)vertex);
		vert->Pos = MU_VECT3DF(pos);
	}
}

void Vertices_SetTCoords(IntPtr vertex, M_POS2DF tcoords, bool vertor2t)
{
	if(vertor2t)
	{
		S3DVertex *vert = ((S3DVertex*)vertex);
		irr::core::position2d<float> c = MU_POS2DF(tcoords);
		vert->TCoords = irr::core::vector2d<float>(c.X, c.Y);
	}
	else
	{
		S3DVertex2TCoords *vert = ((S3DVertex2TCoords*)vertex);
		irr::core::position2d<float> c = MU_POS2DF(tcoords);
		vert->TCoords = irr::core::vector2d<float>(c.X, c.Y);
	}
}

void Vertices_SetTCoords2(IntPtr vertex, M_POS2DF tcoords)
{
	S3DVertex2TCoords *vert = ((S3DVertex2TCoords*)vertex);	
	irr::core::position2d<float> c = MU_POS2DF(tcoords);
	vert->TCoords2 = irr::core::vector2d<float>(c.X, c.Y);
}
