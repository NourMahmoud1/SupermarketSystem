
select p.ProdID, p.ProdName , p.ProdQty , p.ProdPrice, c.CategoryName , s.SupplierName 
From Product as p ,Supplier as s ,Category as c, Supplier_Product as sp where
p.ProdID = sp.ProdID and
p.CategoryID = c.CategoryID and
s.SupplierID = sp.SupplierID 


update Product
set ProdName = 'kdjffhhhhhh'
From Product 
/*join Category on Product.CategoryID = Category.CategoryID */
where ProdID = '10'

delete Product where ProdID = '10'