# RaccTracing Project - Personal Ray Tracing Engine 

This project started while studying "Ray Tracing in One Weekend" book series by Peter Shirley, Trevor David Black and Steve Hollasch. I wanted to put my own spin at it thus it's written in C# in Clean Architecture (as much as possible).
# RaccTracing v1 Preview
Progres of v1 version of a project (v1 covers features from the first book)
![sheeeesh](https://github.com/user-attachments/assets/16f35781-6332-4cf9-b17c-f6b45b2dfed7)
Final Render \
Progress: \
![normal ball](https://github.com/user-attachments/assets/55419081-25f8-45f8-8c6e-85aaa8bf4a8f)
![antialiasing](https://github.com/user-attachments/assets/27e64973-4b40-460a-9e81-347e98b645f6)
![matte](https://github.com/user-attachments/assets/1a25c3ae-7688-465c-a26f-d25e4af8b899)
![matte without accne](https://github.com/user-attachments/assets/ed093b1c-00c5-4b77-b33e-5d4a5e930020)
![bug](https://github.com/user-attachments/assets/cb892850-49f1-4eff-8fd9-d5a5564349c5)
![fuzz](https://github.com/user-attachments/assets/add2bb43-c7a3-4f1a-b76e-22cf663213dd)


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
