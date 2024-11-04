# RaccTracing Project - Personal Ray Tracing Engine 


# Theory

## Ray Class
The class Ray represents the vector of light described by the function \
$P(t) = A + tB$ \
where $P$ is the position in 3D along the line of the vector $vec{AB}$ \ 
where $A$ is the origin of the `Origin` and $B$ the direction of the $Direction$. \
the aforementioned function is called by the `At(double t)` method.

## Vec3 Class
Class for storing geometric vectors and colours. Class used for colours, locations, directions and offsets. 


### Dot method:
The Dot function calculates the scalar product of two vectors. \
\
The scalar product of two non-zero vectors $v$ and $u$ is called the product of the lengths of these vectors multiplied by the cosine of the angle between the vectors, i.e. \
$v \cdot u = |v| \cdot |u| \cdot cos(v,u)$.  \
Two non-zero vectors $v$ and $u$ are perpendicular if and only if their scalar product is zero. \

**Important**. \
If $v = [v_x,v_y,v_z]$ and $u = [u_x, u_y, u_z]$ are vectors in the space ${rm I}^3$, then their scalar product is a number given by the formula: \
$v \circ u = v_xu_x+v_yu_y+v_zu_z$  \
\
This theorem is used in the `Dot(Vec3 a, Vec3 b)` method. 
### Cross method 
The Cross method calculates the vector product of two vectors. \
\
The coordinates of the vector product of vectors $v = [v_x,v_y,v_z]$ and $u = [u_x, u_y, u_z]$ can be written using the determinant: \
$v \times u =$ \
| i   j   k  | \
| $v_x  v_y  v_z$ | \
| $u_x  u_y  u_z$ | \
It follows from the above theorem that the coordinates of the vector: \
$w=v \times u = [w_x, w_y, w_z]$ \
are expressed by the formulas: \
$w_x = v_yu_z- v_zu_y, w_y=v_zu_x - v_xu_z, w_z = v_xu_y - v_yu_x$ \ 
### Length and LengthSquared method 
Length is calculated using Pythagoras' theorem 

### UnitVector method 
The `UnitVector` method returns a unit vector, i.e. a vector of length one, indicating the direction and return of some initial vector to which this vector is assigned. Multiplying the unit vector by the length of the initial vector reconstructs the initial vector. 
## Color and Point3 Classes
Both classes are aliases of Vec3 class.
