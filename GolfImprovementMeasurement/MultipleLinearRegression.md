The method for calculating the "best-fit" approximation for a list of $(x, y, z)$ data points is typically done using **Multiple Linear Regression** or the **Ordinary Least Squares (OLS)** method, which finds the plane that minimizes the sum of the squared vertical distances (residuals) from each point to the plane.

Assuming you're looking for a plane of the form:
$$z = \beta_0 + \beta_1 x + \beta_2 y$$
where $\beta_0$, $\beta_1$, and $\beta_2$ are the coefficients (parameters) you need to calculate.

This problem is most efficiently solved using **matrix algebra** and the **Normal Equation**.

***

## 1. Set up the Data in Matrix Form

For $n$ data points $(x_i, y_i, z_i)$, you will construct three matrices:

1.  **Response Vector ($\mathbf{Y}$):** A column vector of all the $z$-values.
    $$\mathbf{Y} = \begin{pmatrix} z_1 \\ z_2 \\ \vdots \\ z_n \end{pmatrix}$$

2.  **Design Matrix ($\mathbf{X}$):** An $n \times 3$ matrix of the predictor variables. The first column is all ones (for the intercept $\beta_0$), and the following columns are the $x$ and $y$ values.
    $$\mathbf{X} = \begin{pmatrix} 1 & x_1 & y_1 \\ 1 & x_2 & y_2 \\ \vdots & \vdots & \vdots \\ 1 & x_n & y_n \end{pmatrix}$$

3.  **Coefficient Vector ($\boldsymbol{\beta}$):** A column vector of the unknown coefficients.
    $$\boldsymbol{\beta} = \begin{pmatrix} \beta_0 \\ \beta_1 \\ \beta_2 \end{pmatrix}$$

The model is represented as $\mathbf{Y} \approx \mathbf{X} \boldsymbol{\beta}$.

***

## 2. The Normal Equation

The OLS estimate for the coefficient vector $\boldsymbol{\beta}$ is given by the **Normal Equation**:

$$\hat{\boldsymbol{\beta}} = \left( \mathbf{X}^\top \mathbf{X} \right)^{-1} \mathbf{X}^\top \mathbf{Y}$$

Here's what each part means:

* $\mathbf{X}^\top$: The **transpose** of the design matrix $\mathbf{X}$ (rows become columns and columns become rows).
* $\mathbf{X}^\top \mathbf{X}$: The matrix product of $\mathbf{X}^\top$ and $\mathbf{X}$. This will be a $3 \times 3$ matrix.
* $\left( \mathbf{X}^\top \mathbf{X} \right)^{-1}$: The **inverse** of the $3 \times 3$ matrix $\mathbf{X}^\top \mathbf{X}$.
* $\mathbf{X}^\top \mathbf{Y}$: The matrix product of $\mathbf{X}^\top$ and $\mathbf{Y}$. This will be a $3 \times 1$ vector.
* $\hat{\boldsymbol{\beta}}$: The final vector of estimated coefficients: $\hat{\boldsymbol{\beta}} = \begin{pmatrix} \hat{\beta}_0 \\ \hat{\beta}_1 \\ \hat{\beta}_2 \end{pmatrix}$.

The resulting values $\hat{\beta}_0$, $\hat{\beta}_1$, and $\hat{\beta}_2$ are the coefficients for your best-fit plane:

$$\hat{z} = \hat{\beta}_0 + \hat{\beta}_1 x + \hat{\beta}_2 y$$

***
